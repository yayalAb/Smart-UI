using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.UserGroupModule.Queries.UserGroupLookup;

namespace Application.UserGroupModule.Queries.GetShippingAgentLookupQuery;

public record GetShippingAgentLookupQuery : IRequest<List<DropDownLookupDto>> {}

public class GetShippingAgentLookupQueryHandler : IRequestHandler<GetShippingAgentLookupQuery, List<DropDownLookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentLookupQueryHandler(IMapper mapper, IAppDbContext context) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(GetShippingAgentLookupQuery request, CancellationToken candellationToken) {
        return await _context.ShippingAgents.Select(u => new DropDownLookupDto() {
            Text = u.FullName,
            Value = u.Id
        }).ToListAsync();
    }

}
