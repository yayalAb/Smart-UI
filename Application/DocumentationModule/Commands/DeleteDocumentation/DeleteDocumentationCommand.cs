

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.DocumentationModule.Commands.DeleteDocumentation
{
    public record DeleteDocumentationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteDocumentationCommandHandler : IRequestHandler<DeleteDocumentationCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteDocumentationCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteDocumentationCommand request, CancellationToken cancellationToken)
        {
            var existingDocumentation = await _context.Documentations.FindAsync(request.Id);
            if (existingDocumentation == null)
            {
                throw new NotFoundException("Documentation", new { Id = request.Id });

            };

            _context.Documentations.Remove(existingDocumentation);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
