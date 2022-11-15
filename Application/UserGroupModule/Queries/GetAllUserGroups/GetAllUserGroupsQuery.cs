
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetAllUserGroups
{
    public record GetAllUserGroupsQuery : IRequest<IEnumerable<UserGroupDto>>
    {

    }
    public class GetAllUserGroupsCommandHandler : IRequestHandler<GetAllUserGroupsQuery, IEnumerable<UserGroupDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUserGroupsCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserGroupDto>> Handle(GetAllUserGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _context.UserGroups
                .Include(ug => ug.UserRoles)
                .ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
