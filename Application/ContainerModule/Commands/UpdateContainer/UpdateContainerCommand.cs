
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ContainerModule.Commands.UpdateContainer
{
    public record UpdateContainerCommand : IRequest<int>
    {
        public int Id { get; init; }
        public string ContianerNumber { get; init; } = null!;
        public float Size { get; init; }
        public string? Owner { get; init; }
        public int LocationPortId { get; init; }
        public int OperationId { get; init; }
        public DateTime? ManufacturedDate { get; init; }
        public IFormFile? ImageFile { get; init; }
    }
    public class UpdateContainerCommandHandler : IRequestHandler<UpdateContainerCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public UpdateContainerCommandHandler(IAppDbContext context , IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }
        public async Task<int> Handle(UpdateContainerCommand request, CancellationToken cancellationToken)
        {
            // check if container exists 
            var oldContainer = await _context.Containers.FindAsync(request.Id);
            if(oldContainer == null)
            {
                throw new NotFoundException("Container", new { Id = request.Id });
            }
            // update image 
            if(request.ImageFile != null)
            {
                var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
                if (!response.result.Succeeded)
                {
                    throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                }
                oldContainer.Image = response.byteData;
            }
            // update contianer record
            oldContainer.LocationPortId = request.LocationPortId;
            oldContainer.ContianerNumber = request.ContianerNumber;
            oldContainer.Owner = request.Owner;
            oldContainer.Size = request.Size;
            oldContainer.OperationId = request.OperationId;
            oldContainer.ManufacturedDate = request.ManufacturedDate;
            _context.Containers.Update(oldContainer);
            await  _context.SaveChangesAsync(cancellationToken);
            return oldContainer.Id; 

        }
    }

}
