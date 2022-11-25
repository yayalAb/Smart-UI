using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.GetUnassignedDrivers;
public record GetUnassignedDriversQuery : IRequest<List<DriverDto>>{
    
}
public class GetUnassignedDriversQueryHandler : IRequestHandler<GetUnassignedDriversQuery, List<DriverDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUnassignedDriversQueryHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<DriverDto>> Handle(GetUnassignedDriversQuery request, CancellationToken cancellationToken)
    {
        return await _context.Drivers
            .Where(d => d.IsAssigned == false)
            .Include(d => d.Address)
            .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
