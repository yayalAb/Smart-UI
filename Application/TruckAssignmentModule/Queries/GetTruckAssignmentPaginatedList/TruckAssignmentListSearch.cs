using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TruckAssignmentModule.Queries.GetTruckAssignmentPaginatedList;
public record TruckAssignmentListSearch : IRequest<PaginatedList<TruckAssignmentDto>>
{
    public string Word { get; init; }
    public int PageCount { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class TruckAssignmentListSearchHandler : IRequestHandler<TruckAssignmentListSearch, PaginatedList<TruckAssignmentDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public TruckAssignmentListSearchHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<TruckAssignmentDto>> Handle(TruckAssignmentListSearch request, CancellationToken cancellationToken)
    {
        return await PaginatedList<TruckAssignmentDto>
            .CreateAsync(_context.TruckAssignments
            .Include(ta => ta.Driver)
            .Include(ta => ta.Truck)
            .Include(ta => ta.Operation)
            .Include(ta => ta.SourcePort)
            .Include(ta => ta.DestinationPort)
            .Include(ta => ta.Goods)
            .Include(ta => ta.Containers)!
                .ThenInclude(c => c.Goods)
            .Where(t => t.SourceLocation.Contains(request.Word) ||
                t.DestinationLocation.Contains(request.Word) ||
                (t.TransportationMethod != null ? t.TransportationMethod.Contains(request.Word) : false) ||
                t.AgreedTariff.ToString().Contains(request.Word) ||
                t.Currency.Contains(request.Word) ||
                t.SENumber.Contains(request.Word) ||
                t.GatePassType.Contains(request.Word) ||
                t.Driver.Fullname.Contains(request.Word) ||
                t.Truck.TruckNumber.Contains(request.Word) ||
                t.Operation.OperationNumber.Contains(request.Word) ||
                t.SourcePort.PortNumber.Contains(request.Word) ||
                (t.SourcePort.Country != null ? t.SourcePort.Country.Contains(request.Word) : false) ||
                t.DestinationPort.PortNumber.Contains(request.Word) ||
                (t.DestinationPort.Country != null ? t.DestinationPort.Country.Contains(request.Word) : false)
            )
            .ProjectTo<TruckAssignmentDto>(_mapper.ConfigurationProvider), request.PageCount, request.PageSize);
    }

}