using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;
using Application.Common.Mappings;

namespace Application.ShippingAgentModule.Queries.GetShippingAgentPaginatedList;

public class GetShippingAgentPaginatedListQuery : IRequest<PaginatedList<ShippingAgentDto>> {
    public int pageNumber {get; init; }
    public int pageSize {get; init; }
}

public class GetShippingAgentPaginatedListQueryHandler : IRequestHandler<GetShippingAgentPaginatedListQuery, PaginatedList<ShippingAgentDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentPaginatedListQueryHandler( IAppDbContext context , IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShippingAgentDto>> Handle(GetShippingAgentPaginatedListQuery request, CancellationToken cancellationToken) {
        return await _context.ShippingAgents
        .Include(t => t.Address)
        .ProjectTo<ShippingAgentDto>(_mapper.ConfigurationProvider)
        .PaginatedListAsync(request.pageNumber , request.pageSize);
    }

}