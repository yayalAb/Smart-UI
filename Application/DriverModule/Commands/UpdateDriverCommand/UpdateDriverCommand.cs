using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Application.AddressModule.Commands.AddressCreateCommand;

namespace Application.DriverModule.Commands.UpdateDriverCommand
{
    public record UpdateDriverCommand : IRequest<Driver> {
        public int Id {get; init;}
        public string Fullname { get; init; }
        public string LicenceNumber { get; init; }
        public int TruckId {get; init;}
        public AddressCreateCommand address { get; init; }
    }

    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Driver> {
        
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateDriverCommandHandler> _logger;

        public UpdateDriverCommandHandler(IIdentityService identityService , IAppDbContext context , ILogger<UpdateDriverCommandHandler> logger) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<Driver> Handle(UpdateDriverCommand request, CancellationToken cancellationToken){

            Driver found_driver = _context.Drivers
                        .Include(d => d.Address)
                        .Include(d => d.Truck)
                        .Where(d => d.Id == request.Id)
                        .FirstOrDefault();

            if(found_driver == null){
                throw new Exception("driver not found");
            }

            if(found_driver.Truck == null || found_driver.Truck.Id != request.TruckId){
                
                Truck found_truck = await _context.Trucks.FindAsync(request.TruckId);

                if(found_driver == null){
                    throw new Exception("Truck not found");
                }

                found_driver.TruckId = found_truck.Id;
                found_driver.Truck = null;

            }

            found_driver.Address.Email = request.address.Email;
            found_driver.Address.Phone = request.address.Phone;
            found_driver.Address.Region = request.address.Region;
            found_driver.Address.City = request.address.City;
            found_driver.Address.Subcity = request.address.Subcity;
            found_driver.Address.Country = request.address.Country;
            found_driver.Address.POBOX = request.address.POBOX;

            await _context.SaveChangesAsync(cancellationToken);

            return found_driver;

        }

    }
}