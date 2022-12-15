
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ContainerModule.Commands.UpdateContainer
{
    public record UpdateContainerCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
        public string ContainerNumber { get; init; } = null!;
        public string Location {get; init;} = null!;
        public string SealNumber { get; init; } = null!;
        public string Size { get; set; }
        public int? LocationPortId { get; init; }
        public int OperationId { get; init; }
    }
    public class UpdateContainerCommandHandler : IRequestHandler<UpdateContainerCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public UpdateContainerCommandHandler(IAppDbContext context , IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }
        public async Task<CustomResponse> Handle(UpdateContainerCommand request, CancellationToken cancellationToken)
        {
            // check if container exists 
            var oldContainer = await _context.Containers.FindAsync(request.Id);
            if(oldContainer == null)
            {
                throw new GhionException(CustomResponse.NotFound("Container not found"));
            }
            oldContainer.LocationPortId = request.LocationPortId;
            oldContainer.ContianerNumber = request.ContainerNumber;
            oldContainer.SealNumber = request.SealNumber;
            oldContainer.OperationId = request.OperationId;
            _context.Containers.Update(oldContainer);
            await  _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Container Updated");

        }
    }

}
