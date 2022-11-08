using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.ContactPersonModule.Commands.ContactPersonCreateCommand;
using Application.AddressModule.Commands.AddressCreateCommand;

namespace Application.CompanyModule.Commands.CreateCompanyCommand;

public record CreateCompanyCommand: IRequest<Company> {

    public string? Name { get; init; }
    public string? TinNumber { get; init; }
    public string? CodeNIF { get; init; }
    public ContactPersonCreateCommand contactPerson { get; init; }
    public AddressCreateCommand address { get; init; }

}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Company> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly ILogger<CreateCompanyCommandHandler> _logger;

    public CreateCompanyCommandHandler(IIdentityService identityService , IAppDbContext context , ILogger<CreateCompanyCommandHandler> logger) {
        _identityService = identityService;
        _context = context;
        _logger = logger;
    }
    
    public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken) {

        var transaction = _context.database.BeginTransaction();

        try{

            Address new_address = new Address();

            new_address.Email = request.address.Email;
            new_address.Phone = request.address.Phone;
            new_address.Region = request.address.Region;
            new_address.City = request.address.City;
            new_address.Subcity = request.address.Subcity;
            new_address.Country = request.address.Country;
            new_address.POBOX = request.address.POBOX;

            _context.Addresses.Add(new_address);
            await _context.SaveChangesAsync(cancellationToken);

            ContactPerson new_contact_person = new ContactPerson();
            
            new_contact_person.Name = request.contactPerson.Name;
            new_contact_person.Email = request.contactPerson.Email;
            new_contact_person.Phone = request.contactPerson.Phone;

            _context.ContactPeople.Add(new_contact_person);
            await _context.SaveChangesAsync(cancellationToken);

            Company new_company = new Company();
        
            new_company.Name = request.Name;
            new_company.TinNumber = request.TinNumber;
            new_company.CodeNIF = request.CodeNIF;
            new_company.ContactPersonId = new_contact_person.Id;
            new_company.AddressId = new_address.Id;
            new_company.ContactPerson = new_contact_person;
            new_company.Address = new_address;

            _context.Companies.Add(new_company);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync();

            return new_company;

        }catch(Exception ex){
            await transaction.RollbackAsync();
            throw new CompanyException(ex.Message);
        }

    }
}