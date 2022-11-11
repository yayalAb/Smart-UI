
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
        public string ContianerNumber { get; set; } = null!;
        public float Size { get; set; }
        public string? Owner { get; set; }
        public string? Location { get; set; }
        public DateTime? ManufacturedDate { get; set; }
        public IFormFile? ImageFile { get; set; }
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
            oldContainer.Location = request.Location;
            oldContainer.ContianerNumber = request.ContianerNumber;
            oldContainer.Owner = request.Owner;
            oldContainer.Size = request.Size;
            oldContainer.ManufacturedDate = request.ManufacturedDate;
            _context.Containers.Update(oldContainer);
            await  _context.SaveChangesAsync(cancellationToken);
            return oldContainer.Id; 

        }
    }

}
