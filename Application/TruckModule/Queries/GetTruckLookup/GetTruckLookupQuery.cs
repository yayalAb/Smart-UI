using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.UserGroupModule.Queries.UserGroupLookup;

namespace Application.UserGroupModule.Queries.GetTruckLookupQuery;

public record GetTruckLookupQuery : IRequest<List<DropDownLookupDto>> {}

public class GetTruckLookupQueryHandler : IRequestHandler<GetTruckLookupQuery, List<DropDownLookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTruckLookupQueryHandler(IMapper mapper, IAppDbContext context) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(GetTruckLookupQuery request, CancellationToken candellationToken) {
        return await _context.Trucks.Select(u => new DropDownLookupDto() {
            Text = u.TruckNumber,
            Value = u.Id
        }).ToListAsync();
    }

}
