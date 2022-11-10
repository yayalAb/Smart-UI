using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Application.TruckModule.Commands.ChangeTruckImageCommand
{
    public record ChangeTruckImageCommand : IRequest<Truck> {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

    public class ChangeTruckImageCommandHandler : IRequestHandler<ChangeTruckImageCommand, Truck> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<ChangeTruckImageCommandHandler> _logger;
        private readonly IFileUploadService _fileUploadService;

        public ChangeTruckImageCommandHandler(
            IIdentityService identityService, 
            IAppDbContext context, 
            ILogger<ChangeTruckImageCommandHandler> logger, 
            IFileUploadService fileUploadService
        ) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public async Task<Truck> Handle(ChangeTruckImageCommand request, CancellationToken cancellationToken) {

            Truck found_truck = await _context.Trucks.FindAsync(request.Id);

            if(found_truck == null){
                throw new Exception("Truck not found");
            }

            //image uploading
            var response = await _fileUploadService.uploadFile(request.ImageFile, FileType.Image);
            if (!response.result.Succeeded)
            {
                throw new Exception(String.Join(" , ", response.result.Errors));
            }

            found_truck.ImageId = response.Id;
            await _context.SaveChangesAsync(cancellationToken);

            return found_truck;

        }

    }
}