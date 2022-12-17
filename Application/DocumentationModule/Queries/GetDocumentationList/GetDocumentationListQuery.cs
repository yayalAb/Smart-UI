
using Application.Common.Interfaces;
using Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DocumentationModule.Queries.GetDocumentationList;

public class GetDocumentationListQuery : IRequest<List<DocumentationDto>>
{

}

public class GetDocumentationListQueryHandler : IRequestHandler<GetDocumentationListQuery, List<DocumentationDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetDocumentationListQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocumentationDto>> Handle(GetDocumentationListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Documentations
        .ProjectTo<DocumentationDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }

}