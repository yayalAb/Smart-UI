using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using MediatR;
using Application.AddressModule.Commands.AddressUpdateCommand;
using Application.ContactPersonModule.Commands.ContactPersonUpdateCommand;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Application.Common.Models;
using Application.Common.Exceptions;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using AutoMapper;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;

namespace Application.CompanyModule.Commands.UpdateCompanyCommand {

    public class UpdateCompanyCommand : IRequest<CustomResponse> {
        public int Id { get; init; }
        public string Name { get; init; }
        public string TinNumber { get; init; }
        public string CodeNIF { get; init; }
        public ICollection<ContactPersonUpdateCommand> contactPeople { get; init; }
        public UpdateAddressDto address { get; init; }
        public ICollection<UpdateBankInformationDto> BankInformation { get; init; }
    }


    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CustomResponse> {

        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;
        
        public UpdateCompanyCommandHandler(IMapper mapper , IAppDbContext context , ILogger<UpdateCompanyCommandHandler> logger){

            _mapper = mapper;
            _context = context;
            _logger = logger;

        }

        public async Task<CustomResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken){
            
            bool isCompanyFound = _context.Companies
                        .Include(c => c.Address)
                        .Include(c => c.ContactPeople)
                        .Include( c => c.BankInformation)
                        .Where(c => c.Id == request.Id).AsNoTracking()
                        .Any();
            // var comp = _context.Companies.Find(request.Id);

            if(!isCompanyFound){
                throw new GhionException(CustomResponse.NotFound("Company not found!"));
            }
            _context.Companies.Update(_mapper.Map<Company>(request));
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Company Updated");

        }

    }

}

