using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;
using Application.Common.Mappings;

namespace Application.DocumentationModule.Queries.GetDocumentationPaginatedList;

public class GetDocumentationPaginatedListQuery : IRequest<PaginatedList<DocumentationDto>> {
    public int PageCount {get; init; }
    public int PageSize {get; init; }
}

public class GetDocumentationPaginatedListQueryHandler : IRequestHandler<GetDocumentationPaginatedListQuery, PaginatedList<DocumentationDto>> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetDocumentationPaginatedListQueryHandler( IAppDbContext context , IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DocumentationDto>> Handle(GetDocumentationPaginatedListQuery request, CancellationToken cancellationToken) {
        return await _context.Documentations
        .ProjectTo<DocumentationDto>(_mapper.ConfigurationProvider)
        .PaginatedListAsync(request.PageCount , request.PageSize);
    }

}