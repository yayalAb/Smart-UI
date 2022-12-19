using Application.Common.Interfaces;
using Application.UserGroupModule.Queries.UserGroupLookup;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetTruckLookupQuery;

public record GetTruckLookupQuery : IRequest<List<DropDownLookupDto>> { }

public class GetTruckLookupQueryHandler : IRequestHandler<GetTruckLookupQuery, List<DropDownLookupDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTruckLookupQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(GetTruckLookupQuery request, CancellationToken candellationToken)
    {
        return await _context.Trucks.Select(u => new DropDownLookupDto()
        {
            Text = u.TruckNumber,
            Value = u.Id
        }).ToListAsync();
    }

}
