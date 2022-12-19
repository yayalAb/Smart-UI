using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.PortModule.Queries.GetAllPortsQuery;

public record GetAllPorts : IRequest<PaginatedList<Port>>
{
    public int? PageCount { get; init; } = 1!;
    public int? PageSize { get; init; } = 10!;
}

public class GetAllDriversHandler : IRequestHandler<GetAllPorts, PaginatedList<Port>>
{

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllDriversHandler(
        IIdentityService identityService,
        IAppDbContext context
    )
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<PaginatedList<Port>> Handle(GetAllPorts request, CancellationToken cancellationToken)
    {
        return await PaginatedList<Port>.CreateAsync(_context.Ports, request.PageCount ?? 1, request.PageSize ?? 10);
    }

}