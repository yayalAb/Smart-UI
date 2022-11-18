
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;

namespace Application.DocumentationModule.Queries.GetDocumentationList;

public class GetDocumentationListQuery : IRequest<List<DocumentationDto>> {
    
}

public class GetDocumentationListQueryHandler : IRequestHandler<GetDocumentationListQuery, List<DocumentationDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetDocumentationListQueryHandler( IAppDbContext context , IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocumentationDto>> Handle(GetDocumentationListQuery request, CancellationToken cancellationToken) {
        return await _context.Documentations
        .ProjectTo<DocumentationDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }

}