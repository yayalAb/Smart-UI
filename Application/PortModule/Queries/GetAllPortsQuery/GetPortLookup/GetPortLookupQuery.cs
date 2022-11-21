using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.UserGroupModule.Queries.UserGroupLookup;

namespace Application.UserGroupModule.Queries.GetPortLookupQuery;

public record GetPortLookupQuery : IRequest<List<DropDownLookupDto>> {}

public class GetPortLookupQueryHandler : IRequestHandler<GetPortLookupQuery, List<DropDownLookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetPortLookupQueryHandler(IMapper mapper, IAppDbContext context) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(GetPortLookupQuery request, CancellationToken candellationToken) {
        return await _context.Ports.Select(u => new DropDownLookupDto() {
            Text = u.PortNumber,
            Value = u.Id
        }).ToListAsync();
    }

}
