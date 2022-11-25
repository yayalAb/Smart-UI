using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.UserGroupModule.Queries.UserGroupLookup;
using Application.TruckModule;

namespace Application.TruckModule.Queries.GetUnassignedTrucks;

public record GetUnassignedTrucksQuery : IRequest<List<TruckDto>> {}

public class GetUnassignedTrucksQueryHandler : IRequestHandler<GetUnassignedTrucksQuery, List<TruckDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUnassignedTrucksQueryHandler(IMapper mapper, IAppDbContext context) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TruckDto>> Handle(GetUnassignedTrucksQuery request, CancellationToken candellationToken) {
        return await _context.Trucks
            .Where(t => t.IsAssigned == false)
            .ProjectTo<TruckDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

}
