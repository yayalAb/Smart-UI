using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TruckAssignmentModule.Queries.GetTruckAssignmentPaginatedList;
public record GetTruckAssignmentPaginatedListQuery : IRequest<PaginatedList<TruckAssignmentDto>>
{
    public int PageCount { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetTruckAssignmentPaginatedListQueryHandler : IRequestHandler<GetTruckAssignmentPaginatedListQuery, PaginatedList<TruckAssignmentDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTruckAssignmentPaginatedListQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<TruckAssignmentDto>> Handle(GetTruckAssignmentPaginatedListQuery request, CancellationToken cancellationToken)
    {
        return await PaginatedList<TruckAssignmentDto>
        .CreateAsync(_context.TruckAssignments
        .Include(ta => ta.Driver)
        .Include(ta => ta.Truck)
        .Include(ta => ta.Operation)
        .Include(ta => ta.SourcePort)
        .Include( ta => ta.DestinationPort)
        .Include(ta => ta.Goods)
        .Include(ta => ta.Containers)!
            .ThenInclude(c =>c.Goods)
        .ProjectTo<TruckAssignmentDto>(_mapper.ConfigurationProvider), request.PageCount, request.PageSize);
    }

}