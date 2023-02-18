using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Project.Query.GetAllProjects;

public record GetAllProjects : IRequest<PaginatedList<ProjectsDto>>
{
  
    public record GetAllProjectsQuery : IRequest<List<ProjectsDto>>
    {
    }
    public class GetAllUserGroupsCommandHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectsDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUserGroupsCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProjectsDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _context.UserGroups
                .Include(ug => ug.UserRoles)
                .ProjectTo<ProjectsDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
