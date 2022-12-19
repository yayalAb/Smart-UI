
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.ShippingAgentModule.Queries.GetShippingAgentList;

public class GetShippingAgentListQuery : IRequest<List<ShippingAgentDto>>
{

}

public class GetShippingAgentListQueryHandler : IRequestHandler<GetShippingAgentListQuery, List<ShippingAgentDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentListQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ShippingAgentDto>> Handle(GetShippingAgentListQuery request, CancellationToken cancellationToken)
    {
        var shippingAgents = await _context.ShippingAgents
        .Include(t => t.Address)
        .ToListAsync();
        return shippingAgents.ToShippingAgentDto();
    }

}