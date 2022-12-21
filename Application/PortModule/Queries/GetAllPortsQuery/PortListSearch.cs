using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.PortModule.Queries.GetAllPortsQuery;

public record PortListSearch : IRequest<PaginatedList<Port>>
{
    public string Word { get; init; }
    public int? PageCount { get; init; } = 1!;
    public int? PageSize { get; init; } = 10!;
}

public class PortListSearchHandler : IRequestHandler<PortListSearch, PaginatedList<Port>>
{

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public PortListSearchHandler(
        IIdentityService identityService,
        IAppDbContext context
    )
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<PaginatedList<Port>> Handle(PortListSearch request, CancellationToken cancellationToken) {
        return await PaginatedList<Port>.CreateAsync(
            _context.Ports
                .Where(p => p.PortNumber.Contains(request.Word) ||
                    (p.Country != null ? p.Country.Contains(request.Word) : false) ||
                    (p.Region != null ? p.Region.Contains(request.Word) : false) ||
                    (p.Vollume != null ? p.Vollume.Contains(request.Word) : false)
                ),
            request.PageCount ?? 1, 
            request.PageSize ?? 10);
    }

}