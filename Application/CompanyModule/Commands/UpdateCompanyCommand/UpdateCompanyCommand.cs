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
        public ICollection<ContactPersonUpdateCommand> ContactPeople { get; init; }
        public UpdateAddressDto Address { get; init; }
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
            
            // bool isCompanyFound = _context.Companies
            //             .Include(c => c.Address)
            //             .Include(c => c.ContactPeople)
            //             .Include( c => c.BankInformation)
            //             .Where(c => c.Id == request.Id).AsNoTracking()
            //             .Any();
            // var comp = _context.Companies.Find(request.Id);
  
            // if(!isCompanyFound){
            //     throw new GhionException(CustomResponse.NotFound("Company not found!"));
            // }
             var  existingCompany  = _context.Companies
                        .Include(c => c.Address)
                        .Include(c => c.ContactPeople)
                        .Include( c => c.BankInformation)
                        .Where(c => c.Id == request.Id)
                        .AsNoTracking()
                        .FirstOrDefault();

         var trash =   existingCompany.ContactPeople.ToList()
                .Where(cp => !request.ContactPeople.Where(c => c.Id == cp.Id).Any());
        _context.ContactPeople.RemoveRange(trash);
         await _context.SaveChangesAsync(cancellationToken);


         var trash2 =   existingCompany.BankInformation.ToList()
                .Where(cp => !request.BankInformation.Where(c => c.Id == cp.Id).Any());
        _context.BankInformation.RemoveRange(trash2);
         await _context.SaveChangesAsync(cancellationToken);


            // existingCompany.Address.City = request.address.City;
            // existingCompany.Address.Country = request.address.Country;
            // existingCompany.Address.Email = request.address.Email;
            // existingCompany.Address.Phone = request.address.Phone;
            // existingCompany.Address.Region = request.address.Phone;
            // exi


            existingCompany.Name = request.Name;
            existingCompany.TinNumber = request.TinNumber;
            existingCompany.CodeNIF = request.CodeNIF;
            existingCompany.Address = _mapper.Map<Address>(request.Address);
            existingCompany.ContactPeople = _mapper.Map<ICollection<ContactPerson>>(request.ContactPeople);
            existingCompany.BankInformation = _mapper.Map<ICollection<BankInformation>>(request.BankInformation);
            _context.Companies.Update(existingCompany);
            // _context.Companies.Update(_mapper.Map<Company>(request));
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Company Updated");

        }

    }

}

