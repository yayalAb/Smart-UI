using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.GetDriverQuery;

public record GetDriver : IRequest<DriverDto>
{
    public int Id { get; init; }
}

public class GetDriverHandler : IRequestHandler<GetDriver, DriverDto>
{

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetDriverHandler(
        IMapper mapper,
        IAppDbContext context
    )
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<DriverDto> Handle(GetDriver request, CancellationToken cancellationToken)
    {

        var driver = await _context.Drivers
        .Include(d => d.Address)
        .Where(t => t.Id == request.Id)
        .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();
        if (driver == null)
        {
            throw new GhionException(CustomResponse.NotFound("driver not found!"));
        }

        return driver;

    }

}