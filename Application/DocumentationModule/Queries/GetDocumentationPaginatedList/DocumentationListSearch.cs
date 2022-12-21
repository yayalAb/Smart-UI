using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.DocumentationModule.Queries.GetDocumentationPaginatedList;

public class DocumentationListSearch : IRequest<PaginatedList<DocumentationDto>>
{
    public string Word { get; init; }
    public int PageCount { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class DocumentationListSearchHandler : IRequestHandler<DocumentationListSearch, PaginatedList<DocumentationDto>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public DocumentationListSearchHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DocumentationDto>> Handle(DocumentationListSearch request, CancellationToken cancellationToken)
    {
        return await _context.Documentations
            .Where(d => d.Type.Contains(request.Word) ||
                (d.BankPermit != null ? d.BankPermit.Contains(request.Word) : false) ||
                (d.InvoiceNumber != null ? d.InvoiceNumber.Contains(request.Word) : false) ||
                (d.Source != null ? d.Source.Contains(request.Word) : false) ||
                (d.Destination != null ? d.Destination.Contains(request.Word) : false) ||
                (d.PurchaseOrderNumber != null ? d.PurchaseOrderNumber.Contains(request.Word) : false) ||
                (d.PaymentTerm != null ? d.PaymentTerm.Contains(request.Word) : false) ||
                (d.Fright != null ? d.Fright.Contains(request.Word) : false)
            )
            .ProjectTo<DocumentationDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageCount, request.PageSize);
    }

}