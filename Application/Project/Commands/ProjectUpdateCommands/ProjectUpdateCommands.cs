using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Project.Commands.ProjectUpdateCommands
{
    public class ProjectUpdateCommands : IRequest<CustomResponse>
    {
        public int Id { get; set; }
        public string ProjectName { get; init; 
    }
    public class ProjectUpdateCommandsHandler : IRequestHandler<ProjectUpdateCommands, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public ProjectUpdateCommandsHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(ProjectUpdateCommands request, CancellationToken cancellationToken)
        {

            var SelectedProject = await _context.Projects.FindAsync(request.Id);
            if (SelectedProject == null)
            {
                throw new GhionException(CustomResponse.NotFound("Project not found!"));
            };
            SelectedProject.ProjectName = request.ProjectName;
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Project Updated Successfully!");

        }
    }
    }
}

