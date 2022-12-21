
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetUserGroupPaginatedList
{
    public record UserGroupListSearch : IRequest<PaginatedList<UserGroupDto>>
    {
        public string Word { get; init; }
        public int PageCount { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class UserGroupListSearchHandler : IRequestHandler<UserGroupListSearch, PaginatedList<UserGroupDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UserGroupListSearchHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<UserGroupDto>> Handle(UserGroupListSearch request, CancellationToken cancellationToken)
        {
            return await PaginatedList<UserGroupDto>.CreateAsync(_context.UserGroups
                .Include(ug => ug.UserRoles)
                .Where(g => g.Name.Contains(request.Word) ||
                    g.Responsiblity.Contains(request.Word)
                )
                .ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider)
                , request.PageCount, request.PageSize);
        }
    }
}
