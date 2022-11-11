

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.DocumentationModule.Queries.GetDocumentationById
{
    public record GetDocumentationByIdQuery : IRequest<Documentation>
    {
        public int Id { get; set; } 
    }
    public class GetDocumentationByIdQueryHandler : IRequestHandler<GetDocumentationByIdQuery, Documentation>
    {
        private readonly IAppDbContext _context;

        public GetDocumentationByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Documentation> Handle(GetDocumentationByIdQuery request, CancellationToken cancellationToken)
        {
            var doc = await _context.Documentations.FindAsync(request.Id);
            if(doc == null)
            {
                throw new NotFoundException("Documentation", new { id = request.Id });
            }
            return doc; 
        }
    }
}
