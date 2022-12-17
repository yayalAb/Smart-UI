using Application.Common.Interfaces;
using Application.UserGroupModule.Queries.UserGroupLookup;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetShippingAgentLookupQuery;

public record GetShippingAgentLookupQuery : IRequest<List<DropDownLookupDto>> { }

public class GetShippingAgentLookupQueryHandler : IRequestHandler<GetShippingAgentLookupQuery, List<DropDownLookupDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentLookupQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(GetShippingAgentLookupQuery request, CancellationToken candellationToken)
    {
        return await _context.ShippingAgents.Select(u => new DropDownLookupDto()
        {
            Text = u.FullName,
            Value = u.Id
        }).ToListAsync();
    }

}
