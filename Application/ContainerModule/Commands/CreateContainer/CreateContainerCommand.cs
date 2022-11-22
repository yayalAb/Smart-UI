using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.GoodModule.Commands.CreateGoodCommand;
using AutoMapper;
using Application.Common.Models;

namespace Application.ContainerModule.Commands.CreateContainer
{
    public record  CreateContainerCommand : IRequest<CustomResponse>
    {
        public string ContianerNumber { get; set; }
        public float Size { get; set; }
        public string? Owner { get; set; }
        public int LocationPortId { get; set; }
        public int OperationId { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public ICollection<CreateGoodCommand> Goods {get; set;}
    }

    public class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand, CustomResponse> {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public CreateContainerCommandHandler(IAppDbContext context , IFileUploadService fileUploadService, IMapper mapper)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }
        public async Task<CustomResponse> Handle(CreateContainerCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.database.BeginTransaction();

            try
            {
                // byte[]? imageData = null;
                // //get imagebyte data
                // if (request.ImageFile != null)
                // {
                //     var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
                //     if (!response.result.Succeeded)
                //     {
                //         throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                //     }
                //     imageData = response.byteData;
                // }

                Container newContainer = new Container()
                {
                    ContianerNumber = request.ContianerNumber,
                    Size = request.Size,
                    Owner = request.Owner,
                    LocationPortId = request.LocationPortId,
                    ManufacturedDate = request.ManufacturedDate,
                    OperationId = request.OperationId, 
                };

                _context.Containers.Add(newContainer);
                await _context.SaveChangesAsync(cancellationToken);

                //inserting goods

                foreach(var good in request.Goods){
                    good.ContainerId = newContainer.Id;
                    _context.Goods.Add(_mapper.Map<Good>(good));
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync();
                return CustomResponse.Succeeded("Container Created");

            } catch (Exception) {
                transaction.Rollback();
                throw;
            }

            
        }
    }
}
