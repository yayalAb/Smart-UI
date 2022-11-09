using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.TruckModule.Commands.CreateTruckCommand
{
    public record CreateTruckCommand : IRequest<Truck> {
        public string TruckNumber { get; init; }
        public string Type { get; init; }
        public float Capacity { get; init; }
    }

    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Truck> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateTruckCommandHandler> _logger;

        public CreateTruckCommandHandler(IIdentityService identityService , IAppDbContext context , ILogger<CreateTruckCommandHandler> logger) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<Truck> Handle(CreateTruckCommand request, CancellationToken cancellationToken) {

            Truck tr = new Truck();
            tr.TruckNumber = request.TruckNumber;
            tr.Type = request.Type;
            tr.Capacity = request.Capacity;

            _context.Trucks.Add(tr);
            await _context.SaveChangesAsync(cancellationToken);

            return tr;

        }

    }
}