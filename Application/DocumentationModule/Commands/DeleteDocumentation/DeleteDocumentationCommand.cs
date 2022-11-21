

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.DocumentationModule.Commands.DeleteDocumentation
{
    public record DeleteDocumentationCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteDocumentationCommandHandler : IRequestHandler<DeleteDocumentationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeleteDocumentationCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeleteDocumentationCommand request, CancellationToken cancellationToken)
        {
            var found_Documentation = await _context.Documentations.FindAsync(request.Id);
        if(found_Documentation == null){
            throw new GhionException(CustomResponse.NotFound($"Documentation with id = {request.Id} is not found"));
        }
        _context.Documentations.Remove(found_Documentation);
            await _context.SaveChangesAsync(cancellationToken);

         return CustomResponse.Succeeded("Documentation deleted successfully!");

        }
    }

}
