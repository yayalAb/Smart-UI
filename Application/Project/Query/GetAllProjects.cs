using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Project.Query.GetAllProjects;

public record GetAllProjects : IRequest<PaginatedList<ProjectsDto>>
{
    public int? PageCount { set; get; } = 1!;
    public int? PageSize { get; set; } = 10!;
}
public class GetAllProjectsHandler : IRequestHandler<GetAllProjects, PaginatedList<ProjectsDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProjectsHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<ProjectsDto>> Handle(GetAllProjects request, CancellationToken cancellationToken)
    {
        return await PaginatedList<ProjectsDto>
        .CreateAsync(_context.Projects.ProjectTo<ProjectsDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
    }
}
