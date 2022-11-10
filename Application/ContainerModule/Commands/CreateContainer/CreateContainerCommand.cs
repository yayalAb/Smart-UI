﻿
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ContainerModule.Commands.CreateContainer
{
    public record  CreateContainerCommand : IRequest<int>
    {
        public string ContianerNumber { get; set; } = null!;
        public float Size { get; set; }
        public string? Owner { get; set; }
        public string? Loacation { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
    public class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public CreateContainerCommandHandler(IAppDbContext context , IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }
        public async Task<int> Handle(CreateContainerCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.database.BeginTransaction();
            try
            {
                var imageId = 0;
                /// save image to db and retrive id
                if (request.ImageFile != null)
                {
                    var response = await _fileUploadService.uploadFile(request.ImageFile, FileType.Image);
                    if (!response.result.Succeeded)
                    {
                        throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                    }
                    imageId = response.Id;
                }

                Container newContainer = new Container()
                {
                    ContianerNumber = request.ContianerNumber,
                    Size = request.Size,
                    Owner = request.Owner,
                    Loacation = request.Loacation,
                    ManufacturedDate = request.ManufacturedDate,
                    ImageId = imageId != 0 ? imageId : null,    
                };

                await _context.Containers.AddAsync(newContainer);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
                return newContainer.Id;

            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}
