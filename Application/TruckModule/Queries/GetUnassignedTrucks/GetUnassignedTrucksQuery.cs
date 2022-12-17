using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TruckModule.Queries.GetUnassignedTrucks;

public record GetUnassignedTrucksQuery : IRequest<List<UnAssignedTruckDto>> { }

public class GetUnassignedTrucksQueryHandler : IRequestHandler<GetUnassignedTrucksQuery, List<UnAssignedTruckDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUnassignedTrucksQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UnAssignedTruckDto>> Handle(GetUnassignedTrucksQuery request, CancellationToken candellationToken)
    {
        return await _context.Trucks
            .Where(t => t.IsAssigned == false)
            .ProjectTo<UnAssignedTruckDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

}
