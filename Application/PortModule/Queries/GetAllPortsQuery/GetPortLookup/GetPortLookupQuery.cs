using Application.Common.Interfaces;
using Application.UserGroupModule.Queries.UserGroupLookup;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetPortLookupQuery;

public record GetPortLookupQuery : IRequest<List<DropDownLookupDto>> { }

public class GetPortLookupQueryHandler : IRequestHandler<GetPortLookupQuery, List<DropDownLookupDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetPortLookupQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(GetPortLookupQuery request, CancellationToken candellationToken)
    {
        return await _context.Ports.Select(u => new DropDownLookupDto()
        {
            Text = u.PortNumber,
            Value = u.Id
        }).ToListAsync();
    }

}
