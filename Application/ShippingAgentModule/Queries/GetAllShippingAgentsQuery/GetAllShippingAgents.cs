using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShippingAgentModule.Queries.GetAllShippingAgentsQuery;

public class GetAllShippingAgents : IRequest<List<ShippingAgent>> {}

public class GetAllShippingAgentsHandler : IRequestHandler<GetAllShippingAgents, List<ShippingAgent>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllShippingAgentsHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<List<ShippingAgent>> Handle(GetAllShippingAgents request, CancellationToken cancellationToken) {
        return await _context.ShippingAgents.Include(t => t.Address).ToListAsync();
    }

}