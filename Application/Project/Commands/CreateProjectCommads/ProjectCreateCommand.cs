using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Project.Commands.ProjectCreateCommand

{
    public record ProjectCreateCommand : IRequest<CustomResponse>
    {
        public string ProjectName { get; init; }
    }
    public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public ProjectCreateCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
        {
            ProjectModel newProject = new ProjectModel
            {
                ProjectName = request.ProjectName
            };
            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Project created!");

        }
    }
}
