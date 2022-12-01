using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using Application.Common.Exceptions;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.DriverModule.Queries.GetDriverQuery;

public record GetDriver : IRequest<DriverDto> {
    public int Id {get; init;}
}

public class GetDriverHandler : IRequestHandler<GetDriver, DriverDto> {

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetDriverHandler(
        IMapper mapper, 
        IAppDbContext context
    ){
        _mapper = mapper;
        _context = context;
    }

    public async Task<DriverDto> Handle(GetDriver request, CancellationToken cancellationToken) {
        
        var driver = await _context.Drivers
        .Include(d => d.Address)
        .Where(t => t.Id == request.Id)
        .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();
        if(driver == null){
            throw new GhionException(CustomResponse.NotFound("driver not found!"));
        }

        return driver;

    }

}