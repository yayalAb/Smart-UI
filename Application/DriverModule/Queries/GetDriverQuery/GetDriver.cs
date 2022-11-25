using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using Application.Common.Exceptions;

namespace Application.DriverModule.Queries.GetDriverQuery;

public record GetDriver : IRequest<Driver> {
    public int Id {get; init;}
}

public class GetDriverHandler : IRequestHandler<GetDriver, Driver> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetDriverHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<Driver> Handle(GetDriver request, CancellationToken cancellationToken) {
        
        var driver = await _context.Drivers.Include(d => d.Address).Where(t => t.Id == request.Id).FirstOrDefaultAsync();
        if(driver == null){
            throw new GhionException(CustomResponse.NotFound("driver not found!"));
        }

        return driver;

    }

}