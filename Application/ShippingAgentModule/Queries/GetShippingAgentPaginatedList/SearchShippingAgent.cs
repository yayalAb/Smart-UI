using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.ShippingAgentModule.Queries.GetShippingAgentPaginatedList;

public class SearchShippingagent : IRequest<PaginatedList<ShippingAgentDto>>
{
    public string Word {get; init; }
    public int PageCount { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class SearchShippingagentHandler : IRequestHandler<SearchShippingagent, PaginatedList<ShippingAgentDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public SearchShippingagentHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShippingAgentDto>> Handle(SearchShippingagent request, CancellationToken cancellationToken)
    {
        var shippingAgents = await _context.ShippingAgents
        .Include(t => t.Address)
        .Where(s => (s.Address.Phone.Contains(request.Word)) ||
            (s.CompanyName != null ? s.CompanyName.Contains(request.Word) : false) ||
            (s.Address.Country.Contains(request.Word)) ||
            (s.Address.Email.Contains(request.Word)) ||
            (s.FullName.Contains(request.Word))
        ).ToListAsync();
        
        var paginatedList = await PaginatedList<ShippingAgent>.CreateAsync(shippingAgents, pageCount: request.PageCount, pageSize: request.PageSize);

        List<ShippingAgent> items = paginatedList.Items;
        List<ShippingAgentDto> itemDtos = items.ToShippingAgentDto();
        PaginatedList<ShippingAgentDto> paginatedShippingAgentDtos = new PaginatedList<ShippingAgentDto>(
            items: itemDtos,
            count: paginatedList.TotalCount,
            pageCount: paginatedList.PageCount,
            pageSize: paginatedList.PageCount
        );
        return paginatedShippingAgentDtos;

    }

}