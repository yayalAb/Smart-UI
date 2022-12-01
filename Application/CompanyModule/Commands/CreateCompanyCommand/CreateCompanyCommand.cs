using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.ContactPersonModule.Commands.ContactPersonCreateCommand;
using Application.Common.Models;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using AutoMapper;

namespace Application.CompanyModule.Commands.CreateCompanyCommand;

public record CreateCompanyCommand : IRequest<CustomResponse>
{

    public string Name { get; init; } = null!;
    public string TinNumber { get; init; } = null!;
    public string CodeNIF { get; init; } = null!;
    public ContactPersonCreateCommand? contactPerson { get; init; }
    public AddressDto address { get; init; } = null!;

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
        _context.Companies.Add(_mapper.Map<Company>(request));
        await _context.SaveChangesAsync(cancellationToken);
        return CustomResponse.Succeeded("Company Created",201);

    }
}