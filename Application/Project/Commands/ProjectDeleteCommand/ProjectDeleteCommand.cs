using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Project.Commands.ProjectDeleteCommand
{
    public record ProjectDeleteCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
    }
    public class ProjectDeleteCommandHandler : IRequestHandler<ProjectDeleteCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public ProjectDeleteCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.Id);
            if (project == null)
            {
                throw new GhionException(CustomResponse.NotFound($"project with id = {request.Id} is not found"));
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Project deleted successfully!");
        }
    }
}
