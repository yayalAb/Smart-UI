using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.DriverModule.Commands.CreateDriverCommand
{
    public record CreateDriverCommand : IRequest<Driver> {
        public string Fullname { get; init; }
        public string LicenceNumber { get; init; }
        public AddressCreateCommand address { get; init; }
    }

    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Driver> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateDriverCommandHandler> _logger;

        public CreateDriverCommandHandler(IIdentityService identityService , IAppDbContext context , ILogger<CreateDriverCommandHandler> logger) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<Driver> Handle(CreateDriverCommand request, CancellationToken cancellationToken){

            return new Driver();

        }
    }
}