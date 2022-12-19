using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.DocumentationModule.Queries.GetDocumentationPaginatedList;

public class GetDocumentationPaginatedListQuery : IRequest<PaginatedList<DocumentationDto>>
{
    public int PageCount { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetDocumentationPaginatedListQueryHandler : IRequestHandler<GetDocumentationPaginatedListQuery, PaginatedList<DocumentationDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetDocumentationPaginatedListQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DocumentationDto>> Handle(GetDocumentationPaginatedListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Documentations
        .ProjectTo<DocumentationDto>(_mapper.ConfigurationProvider)
        .PaginatedListAsync(request.PageCount, request.PageSize);
    }

}