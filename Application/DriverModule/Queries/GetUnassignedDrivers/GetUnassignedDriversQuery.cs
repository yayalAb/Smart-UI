using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.GetUnassignedDrivers;
public record GetUnassignedDriversQuery : IRequest<List<UnassignedDriverDto>>{
    
}
public class GetUnassignedDriversQueryHandler : IRequestHandler<GetUnassignedDriversQuery, List<UnassignedDriverDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUnassignedDriversQueryHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<UnassignedDriverDto>> Handle(GetUnassignedDriversQuery request, CancellationToken cancellationToken)
    {
        return await _context.Drivers
            .Where(d => d.IsAssigned == false)
            .Include(d => d.Address)
            .ProjectTo<UnassignedDriverDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
