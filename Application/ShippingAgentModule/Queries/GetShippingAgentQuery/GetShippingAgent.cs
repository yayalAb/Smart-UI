using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShippingAgentModule.Queries.GetShippingAgentQuery;

public class GetShippingAgent : IRequest<ShippingAgent> {
    public int Id {get; set;}

    public GetShippingAgent(int id){
        this.Id = id;
    }

}

public class GetAllShippingAgentsHandler : IRequestHandler<GetShippingAgent, ShippingAgent> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllShippingAgentsHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<ShippingAgent> Handle(GetShippingAgent request, CancellationToken cancellationToken) {
        
        var agent = await _context.ShippingAgents.Include(t => t.Address).Include(t => t.AgentFees).FirstOrDefaultAsync();
        if(agent == null){
            throw new Exception("Agent not found!");
        }

        return agent;

    }

}