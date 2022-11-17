using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;

namespace Application.DriverModule.Queries.GetAllDriversQuery;

public record GetAllDrivers : IRequest<PaginatedList<Driver>> {
    public int? PageCount {get; set;} = 1!;
    public int? PageSize {get; set;} = 10!;
}

public class GetAllDriversHandler : IRequestHandler<GetAllDrivers, PaginatedList<Driver>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllDriversHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<PaginatedList<Driver>> Handle(GetAllDrivers request, CancellationToken cancellationToken) {
        return await PaginatedList<Driver>.CreateAsync(_context.Drivers.Include(t => t.Address), request.PageCount ?? 1, request.PageSize ?? 10);
    }

}