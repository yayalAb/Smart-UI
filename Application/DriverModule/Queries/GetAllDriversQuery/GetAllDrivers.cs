using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.DriverModule.Queries.GetAllDriversQuery;

public record GetAllDrivers : IRequest<PaginatedList<DriverDto>> {
    public int? PageCount {get; set;} = 1!;
    public int? PageSize {get; set;} = 10!;
}

public class GetAllDriversHandler : IRequestHandler<GetAllDrivers, PaginatedList<DriverDto>> {

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetAllDriversHandler(
        IMapper mapper, 
        IAppDbContext context
    ){
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<DriverDto>> Handle(GetAllDrivers request, CancellationToken cancellationToken) {
        return await PaginatedList<DriverDto>
            .CreateAsync(
                _context.Drivers
                .Include(t => t.Address)
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10
                );
    }

}