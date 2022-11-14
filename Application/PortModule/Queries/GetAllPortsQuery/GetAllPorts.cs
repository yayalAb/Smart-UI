using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PortModule.Queries.GetAllPortsQuery;

public class GetAllPorts : IRequest<List<Port>> {}

public class GetAllDriversHandler : IRequestHandler<GetAllPorts, List<Port>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllDriversHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<List<Port>> Handle(GetAllPorts request, CancellationToken cancellationToken) {
        return await _context.Ports.ToListAsync();
    }

}