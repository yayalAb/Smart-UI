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
                .Include(t => t.Address).Select(s => new ShippingAgentDto {
                    Id = s.Id,
                    FullName = s.FullName,
                    CompanyName = s.CompanyName,
                    Email = s.Address.Email,
                    Phone = s.Address.Phone,
                    Country = s.Address.Country
                }), 
            pageCount: request.PageCount, 
            pageSize: request.PageSize
        );

    }

}