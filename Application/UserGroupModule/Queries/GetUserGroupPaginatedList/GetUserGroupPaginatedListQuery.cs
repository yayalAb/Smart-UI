
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetUserGroupPaginatedList
{
    public record GetUserGroupPaginatedListQuery : IRequest<PaginatedList<UserGroupDto>>
    {

    public int PageCount { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    }
    public class GetAllUserGroupsCommandHandler : IRequestHandler<GetUserGroupPaginatedListQuery, PaginatedList<UserGroupDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUserGroupsCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<UserGroupDto>> Handle(GetUserGroupPaginatedListQuery request, CancellationToken cancellationToken)
        {
            return await PaginatedList<UserGroupDto>.CreateAsync( _context.UserGroups
                .Include(ug => ug.UserRoles)
                .ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider)
                , request.PageCount , request.PageSize);
        }
    }
}
