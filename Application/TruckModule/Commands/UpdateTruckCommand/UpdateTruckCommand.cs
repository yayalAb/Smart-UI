using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.TruckModule.Commands.UpdateTruckCommand
{
    public record UpdateTruckCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
        public string TruckNumber { get; init; }
        public string Type { get; init; }
        public float Capacity { get; init; }
        public string PlateNumber { get; set; }
        public string? Image { get; set; }

    }

    public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateTruckCommandHandler> _logger;

        public UpdateTruckCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<UpdateTruckCommandHandler> logger
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<CustomResponse> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {

            var found_truck = await _context.Trucks.FindAsync(request.Id);

            if (found_truck == null)
            {
                throw new GhionException(CustomResponse.NotFound("Truck not found"));
            }

            found_truck.TruckNumber = request.TruckNumber;
            found_truck.Type = request.Type;
            found_truck.Capacity = request.Capacity;
            found_truck.Image = request.Image;
            found_truck.PlateNumber = request.PlateNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Truck Updated");

        }

    }
}