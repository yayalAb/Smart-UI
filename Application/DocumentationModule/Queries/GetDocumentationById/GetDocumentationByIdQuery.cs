

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;

namespace Application.DocumentationModule.Queries.GetDocumentationById
{
    public record GetDocumentationByIdQuery : IRequest<DocumentationDto>
    {
        public int Id { get; set; } 
    }
    public class GetDocumentationByIdQueryHandler : IRequestHandler<GetDocumentationByIdQuery, DocumentationDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetDocumentationByIdQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DocumentationDto> Handle(GetDocumentationByIdQuery request, CancellationToken cancellationToken)
        {
            var doc =  _context.Documentations
            .ProjectTo<DocumentationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefault(d => d.Id == request.Id);
            if(doc == null)
            {
                throw new GhionException(CustomResponse.NotFound("Documentation"));
            }
            return doc;
        }
    }
}
