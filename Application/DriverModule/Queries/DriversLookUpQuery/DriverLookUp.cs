
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.DriverLookUpQuery;

public record DriverLookUp : IRequest<ICollection<DriverLookUpDto>> { }

public record DriverLookUpHandler : IRequestHandler<DriverLookUp, ICollection<DriverLookUpDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public DriverLookUpHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<DriverLookUpDto>> Handle(DriverLookUp request, CancellationToken cancellationToken)
    {
        return await _context.Drivers.ProjectTo<DriverLookUpDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

}