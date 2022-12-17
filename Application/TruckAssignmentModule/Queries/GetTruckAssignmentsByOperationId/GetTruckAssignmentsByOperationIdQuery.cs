using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TruckAssignmentModule.Queries;
public record GetTruckAssignmentsByOperationIdQuery : IRequest<List<TruckAssignmentDto>>
{
    public int OperationId { get; set; }
}
public class GetTruckAssignmentsByOperationIdQueryHandler : IRequestHandler<GetTruckAssignmentsByOperationIdQuery, List<TruckAssignmentDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTruckAssignmentsByOperationIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<TruckAssignmentDto>> Handle(GetTruckAssignmentsByOperationIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.TruckAssignments
         .Include(ta => ta.Driver)
         .Include(ta => ta.Truck)
         .Include(ta => ta.Operation)
         .Include(ta => ta.SourcePort)
         .Include(ta => ta.DestinationPort)
         .Include(ta => ta.Goods)
         .Include(ta => ta.Containers)!
             .ThenInclude(c => c.Goods)
         .Where(ta => ta.OperationId == request.OperationId)
         .ProjectTo<TruckAssignmentDto>(_mapper.ConfigurationProvider)
         .ToListAsync();
    }
}
