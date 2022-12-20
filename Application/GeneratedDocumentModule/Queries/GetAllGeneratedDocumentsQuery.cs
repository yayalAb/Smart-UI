using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GeneratedDocumentModule.Queries;
public record GetAllGeneratedDocumentsQuery : IRequest<PaginatedList<GeneratedDocumentDto>>
{

    public int? PageCount { get; set; }
    public int? PageSize { get; set; }
}
public class GetAllGeneratedDocumentsQueryHandler : IRequestHandler<GetAllGeneratedDocumentsQuery, PaginatedList<GeneratedDocumentDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllGeneratedDocumentsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<GeneratedDocumentDto>> Handle(GetAllGeneratedDocumentsQuery request, CancellationToken cancellationToken)
    {
        return await PaginatedList<GeneratedDocumentDto>.CreateAsync(await _context.GeneratedDocuments
                  .Include(gd => gd.Operation)
                  .Include(gd => gd.DestinationPort)
                  .Include(gd => gd.ContactPerson)
                  .Include(gd => gd.Containers)
                  .Include(gd => gd.GeneratedDocumentsGoods)
                  .ProjectTo<GeneratedDocumentDto>(_mapper.ConfigurationProvider).ToListAsync()
      , request.PageCount ?? 1, request.PageSize ?? 10);
    }
}
