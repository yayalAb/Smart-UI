using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ContactPersonModule.Commands.ContactPersonUpdateCommand;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CompanyModule.Commands.UpdateCompanyCommand
{

    public class UpdateCompanyCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string TinNumber { get; init; }
        public string CodeNIF { get; init; }
        public ICollection<ContactPersonUpdateCommand> ContactPeople { get; init; }
        public UpdateAddressDto Address { get; init; }
        public ICollection<UpdateBankInformationDto> BankInformation { get; init; }
    }


    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CustomResponse>
    {

        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;

        public UpdateCompanyCommandHandler(IMapper mapper, IAppDbContext context, ILogger<UpdateCompanyCommandHandler> logger)
        {

            _mapper = mapper;
            _context = context;
            _logger = logger;

        }

        public async Task<CustomResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {
                        var existingCompany = _context.Companies
                       .Include(c => c.Address)
                       .Include(c => c.ContactPeople)
                       .Include(c => c.BankInformation)
                       .Where(c => c.Id == request.Id)
                       .AsNoTracking()
                       .FirstOrDefault();

                        var trash = existingCompany.ContactPeople.ToList()
                           .Where(cp => !request.ContactPeople.Where(c => c.Id == cp.Id).Any());
                        _context.ContactPeople.RemoveRange(trash);
                        await _context.SaveChangesAsync(cancellationToken);


                        var trash2 = existingCompany.BankInformation.ToList()
                           .Where(cp => !request.BankInformation.Where(c => c.Id == cp.Id).Any());
                        _context.BankInformation.RemoveRange(trash2);
                        await _context.SaveChangesAsync(cancellationToken);


                        existingCompany.Address.Email = request.Address.Email;
                        existingCompany.Address.Phone = request.Address.Phone;
                        existingCompany.Address.Region = request.Address.Region;
                        existingCompany.Address.City = request.Address.City;
                        existingCompany.Address.Subcity = request.Address.Subcity;
                        existingCompany.Address.Country = request.Address.Country;
                        existingCompany.Address.POBOX = request.Address.POBOX;


                        existingCompany.Name = request.Name;
                        existingCompany.TinNumber = request.TinNumber;
                        existingCompany.CodeNIF = request.CodeNIF;
                        existingCompany.ContactPeople = _mapper.Map<ICollection<ContactPerson>>(request.ContactPeople);
                        existingCompany.BankInformation = _mapper.Map<ICollection<BankInformation>>(request.BankInformation);
                        _context.Companies.Update(existingCompany);
                        // _context.Companies.Update(_mapper.Map<Company>(request));
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();

                        return CustomResponse.Succeeded("Company Updated");

                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });

        }

    }

}

