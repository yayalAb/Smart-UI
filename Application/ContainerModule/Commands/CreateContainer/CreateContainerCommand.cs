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
        public string ContianerNumber { get; set; }
        public float Size { get; set; }
        public string? Owner { get; set; }
        public int LocationPortId { get; set; }
        public int OperationId { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
    public class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand, int> {
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
                byte[]? imageData = null;
                //get imagebyte data
                if (request.ImageFile != null)
                {
                    var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
                    if (!response.result.Succeeded)
                    {
                        throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                    }
                    imageData = response.byteData;
                }

                Container newContainer = new Container()
                {
                    ContianerNumber = request.ContianerNumber,
                    Size = request.Size,
                    Owner = request.Owner,
                    LocationPortId = request.LocationPortId,
                    ManufacturedDate = request.ManufacturedDate,
                    OperationId = request.OperationId,
                    Image = imageData,    
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
