using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ContactPersonModule.Commands.ContactPersonCreateCommand;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CompanyModule.Commands.CreateCompanyCommand;

public record CreateCompanyCommand : IRequest<CustomResponse>
{

    public string Name { get; init; } = null!;
    public string TinNumber { get; init; } = null!;
    public string CodeNIF { get; init; } = null!;
    public ICollection<ContactPersonCreateCommand>? ContactPeople { get; init; }
    public AddressDto address { get; init; } = null!;
    public ICollection<BankInformationDto> BankInformation { get; init; } = null!;

}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CustomResponse>
{

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;
    private readonly ILogger<CreateCompanyCommandHandler> _logger;

    public CreateCompanyCommandHandler(IMapper mapper, IAppDbContext context, ILogger<CreateCompanyCommandHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public async Task<CustomResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {

        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {

            using (var transaction = _context.database.BeginTransaction())
            {

                try

                {
                    await _context.Companies.AddAsync(_mapper.Map<Company>(request));
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();

                    return CustomResponse.Succeeded("Company Created Successfully", 201);

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
