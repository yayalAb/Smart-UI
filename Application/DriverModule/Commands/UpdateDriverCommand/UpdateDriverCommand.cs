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
using Application.Common.Models;
using Application.Common.Exceptions;

namespace Application.DriverModule.Commands.UpdateDriverCommand
{
    public record UpdateDriverCommand : IRequest<CustomResponse> {
        public int Id {get; init;}
        public string Fullname { get; init; }
        public string LicenceNumber { get; init; }
        public AddressCreateCommand address { get; init; }
        public byte[]? Image {get; init; }
    }

    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, CustomResponse> {
        
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateDriverCommandHandler> _logger;

        public UpdateDriverCommandHandler(IIdentityService identityService , IAppDbContext context , ILogger<UpdateDriverCommandHandler> logger) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<CustomResponse> Handle(UpdateDriverCommand request, CancellationToken cancellationToken){

            var found_driver = _context.Drivers
                        .Include(d => d.Address)
                        .Where(d => d.Id == request.Id)
                        .FirstOrDefault();

            if(found_driver == null){
                throw new GhionException(CustomResponse.NotFound("driver not found"));
            }

            found_driver.Fullname = request.Fullname;
            found_driver.LicenceNumber = request.LicenceNumber;
            found_driver.Image = request.Image;
            found_driver.Address.Email = request.address.Email;
            found_driver.Address.Phone = request.address.Phone;
            found_driver.Address.Region = request.address.Region;
            found_driver.Address.City = request.address.City;
            found_driver.Address.Subcity = request.address.Subcity;
            found_driver.Address.Country = request.address.Country;
            found_driver.Address.POBOX = request.address.POBOX;



            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Dirver Update Successful");

        }

    }
}