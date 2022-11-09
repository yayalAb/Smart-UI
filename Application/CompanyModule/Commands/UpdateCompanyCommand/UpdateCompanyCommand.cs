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

namespace Application.CompanyModule.Commands.UpdateCompanyCommand {

    public class UpdateCompanyCommand : IRequest<Company> {
        public int Id { get; init; }
        public string Name { get; init; }
        public string TinNumber { get; init; }
        public string CodeNIF { get; init; }
        public ContactPersonUpdateCommand contactPerson { get; init; }
        public AddressUpdateCommand address { get; init; }
    }


    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Company> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;
        
        public UpdateCompanyCommandHandler(IIdentityService identityService , IAppDbContext context , ILogger<UpdateCompanyCommandHandler> logger){

            _identityService = identityService;
            _context = context;
            _logger = logger;

        }

        public async Task<Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken){
            
            var comp = _context.Companies
                        .Include(c => c.Address)
                        .Include(c => c.ContactPerson)
                        .Where(c => c.Id == request.Id)
                        .FirstOrDefault();
            // var comp = _context.Companies.Find(request.Id);

            if(comp == null){
                throw new Exception("Company not found!");
            }

            comp.Name = request.Name;
            comp.TinNumber = request.TinNumber;
            comp.CodeNIF = request.CodeNIF;

            if(comp.ContactPerson == null){
                comp.ContactPerson = new ContactPerson();
                comp.ContactPerson.Name = request.contactPerson.Name;
                comp.ContactPerson.Email = request.contactPerson.Email;
                comp.ContactPerson.Phone = request.contactPerson.Phone;
            }else{
                comp.ContactPerson.Name = request.contactPerson.Name;
                comp.ContactPerson.Email = request.contactPerson.Email;
                comp.ContactPerson.Phone = request.contactPerson.Phone;
            }

            comp.Address.Email = request.address.Email;
            comp.Address.Phone = request.address.Phone;
            comp.Address.Region = request.address.Region;
            comp.Address.City = request.address.City;
            comp.Address.Subcity = request.address.Subcity;
            comp.Address.Country = request.address.Country;
            comp.Address.POBOX = request.address.POBOX;

            await _context.SaveChangesAsync(cancellationToken);

            return comp;

        }

    }

}

