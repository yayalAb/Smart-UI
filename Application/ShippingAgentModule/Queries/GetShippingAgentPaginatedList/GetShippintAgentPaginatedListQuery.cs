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
    public int PageCount {get; init; }
    public int PageSize {get; init; }
}

public class GetShippingAgentPaginatedListQueryHandler : IRequestHandler<GetShippingAgentPaginatedListQuery, PaginatedList<ShippingAgentDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentPaginatedListQueryHandler( IAppDbContext context , IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShippingAgentDto>> Handle(GetShippingAgentPaginatedListQuery request, CancellationToken cancellationToken) {
        var shippingAgents =  _context.ShippingAgents
        .Include(t => t.Address);
       var paginatedList = await PaginatedList<ShippingAgent>.CreateAsync(shippingAgents,pageCount: request.PageCount , pageSize: request.PageSize);
       
       List<ShippingAgent> items = paginatedList.Items;
       List<ShippingAgentDto> itemDtos  = items.ToShippingAgentDto();
    PaginatedList<ShippingAgentDto> paginatedShippingAgentDtos = new PaginatedList<ShippingAgentDto>(
        items: itemDtos ,
        count: paginatedList.TotalCount,
        pageCount: paginatedList.PageCount,
        pageSize: paginatedList.PageCount
    );
    return paginatedShippingAgentDtos;
    
    }

}