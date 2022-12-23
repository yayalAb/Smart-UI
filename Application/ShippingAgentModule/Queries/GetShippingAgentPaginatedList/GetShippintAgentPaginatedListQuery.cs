using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.ShippingAgentModule.Queries.GetShippingAgentPaginatedList;

public class GetShippingAgentPaginatedListQuery : IRequest<PaginatedList<ShippingAgentDto>>
{
    public int PageCount { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetShippingAgentPaginatedListQueryHandler : IRequestHandler<GetShippingAgentPaginatedListQuery, PaginatedList<ShippingAgentDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentPaginatedListQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShippingAgentDto>> Handle(GetShippingAgentPaginatedListQuery request, CancellationToken cancellationToken)
    {
        return await PaginatedList<ShippingAgentDto>.CreateAsync(
                _context.ShippingAgents
                    .Include(t => t.Address)
                    .ProjectTo<ShippingAgentDto>(_mapper.ConfigurationProvider), pageCount: request.PageCount, pageSize: request.PageSize
            );
        // var lookups = await PaginatedList<LookupDto>.CreateAsync(_context.Lookups.Where(l => l.Key != "key").ProjectTo<LookupDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
        // var paginatedList = await PaginatedList<ShippingAgent>.CreateAsync(shippingAgents, pageCount: request.PageCount, pageSize: request.PageSize);

        // List<ShippingAgent> items = paginatedList.Items;
        // List<ShippingAgentDto> itemDtos = items.ToShippingAgentDto();
        // PaginatedList<ShippingAgentDto> paginatedShippingAgentDtos = new PaginatedList<ShippingAgentDto>(
        //     items: itemDtos,
        //     count: paginatedList.TotalCount,
        //     pageCount: paginatedList.PageCount,
        //     pageSize: paginatedList.PageCount
        // );
        // return paginatedShippingAgentDtos;

    }

}