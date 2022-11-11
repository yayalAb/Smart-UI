using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Application.TruckModule.Commands.UpdateTruckCommand
{
    public record UpdateTruckCommand : IRequest<Truck> {
        public int Id { get; set; }
        public string TruckNumber { get; init; }
        public string Type { get; init; }
        public float Capacity { get; init; }
        public IFormFile? ImageFile { get; set; }
    }

    public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, Truck> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateTruckCommandHandler> _logger;
        private readonly IFileUploadService _fileUploadService;

        public UpdateTruckCommandHandler(
            IIdentityService identityService, 
            IAppDbContext context, 
            ILogger<UpdateTruckCommandHandler> logger, 
            IFileUploadService fileUploadService
        ) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public async Task<Truck> Handle(UpdateTruckCommand request, CancellationToken cancellationToken) {

            Truck found_truck = await _context.Trucks.FindAsync(request.Id);

            if(found_truck == null){
                throw new Exception("Truck not found");
            }

            found_truck.TruckNumber = request.TruckNumber;
            found_truck.Type = request.Type;
            found_truck.Capacity = request.Capacity;

            _context.Trucks.Add(found_truck);
            await _context.SaveChangesAsync(cancellationToken);

            return found_truck;

        }

    }
}