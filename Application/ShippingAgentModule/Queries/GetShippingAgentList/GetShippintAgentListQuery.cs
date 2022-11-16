using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;
using Application.Common.Mappings;

namespace Application.ShippingAgentModule.Queries.GetShippingAgentList;

public class GetShippingAgentListQuery : IRequest<PaginatedList<ShippingAgentDto>> {
    public int pageNumber {get; init; }=1;
    public int pageSize {get; init; }=10;
}

public class GetShippingAgentListHandler : IRequestHandler<GetShippingAgentListQuery, PaginatedList<ShippingAgentDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentListHandler( IAppDbContext context , IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShippingAgentDto>> Handle(GetShippingAgentListQuery request, CancellationToken cancellationToken) {
        return await _context.ShippingAgents
        .Include(t => t.Address)
        .ProjectTo<ShippingAgentDto>(_mapper.ConfigurationProvider)
        .PaginatedListAsync(request.pageNumber , request.pageSize);
    }

}