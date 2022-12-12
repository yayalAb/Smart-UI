
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverModule.Queries.GetAllDriversQuery;

public class AllDrivers : IRequest<PaginatedList<Driver>> {
    public int? PageCount {get; set;} = 1!;
    public int? PageSize {get; set;} = 10!;
}

public class AllDriversHandler : IRequestHandler<AllDrivers, PaginatedList<Driver>>
{

    private readonly IAppDbContext _context;

    public AllDriversHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<PaginatedList<Driver>> Handle(AllDrivers request, CancellationToken cancellationToken) {
        return await PaginatedList<Driver>.CreateAsync(_context.Drivers.Select(d => new Driver {
            Id = d.Id,
            Fullname = d.Fullname,
            Created = d.Created
        }), request.PageCount ?? 1, request.PageSize ?? 10);
    }

}