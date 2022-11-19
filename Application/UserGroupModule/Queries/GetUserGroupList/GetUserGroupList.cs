
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetUserGroupList
{
    public record GetUserGroupListQuery : IRequest<List<UserGroupDto>>
    {
    }
    public class GetAllUserGroupsCommandHandler : IRequestHandler<GetUserGroupListQuery, List<UserGroupDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUserGroupsCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserGroupDto>> Handle(GetUserGroupListQuery request, CancellationToken cancellationToken)
        {
            return await _context.UserGroups
                .Include(ug => ug.UserRoles)
                .ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
