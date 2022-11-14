using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.GetAllDriversQuery;

public class GetAllDrivers : IRequest<List<Driver>> {}

public class GetAllDriversHandler : IRequestHandler<GetAllDrivers, List<Driver>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllDriversHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<List<Driver>> Handle(GetAllDrivers request, CancellationToken cancellationToken) {
        return await _context.Drivers.Include(t => t.Address).ToListAsync();
    }

}