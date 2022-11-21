using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.UserGroupModule.Queries.UserGroupLookup;

public record UserGroupLookup : IRequest<List<DropDownLookupDto>> {}

public class UserGroupLookupHandler : IRequestHandler<UserGroupLookup, List<DropDownLookupDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UserGroupLookupHandler(IMapper mapper, IAppDbContext context) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DropDownLookupDto>> Handle(UserGroupLookup request, CancellationToken candellationToken) {
        return await _context.UserGroups.Select(u => new DropDownLookupDto() {
            Text = u.Name,
            Value = u.Id
        }).ToListAsync();
    }

}
