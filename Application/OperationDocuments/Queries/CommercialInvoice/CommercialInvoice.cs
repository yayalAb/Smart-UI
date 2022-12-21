
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationDocuments.Queries.Common;
using Domain.Enums;
using MediatR;

namespace Application.OperationDocuments.Queries.CommercialInvoice;

public record CommercialInvoice : IRequest<CommercialInvoiceDto2>
{
    public int operationId { get; init; }
    //if type is false it means Commercia invoice if it is true it means Proforma invoice
    public string? PINumber { get; init; }
    public DateTime? PIDate { get; init; }
    public int TruckAssignmentId { get; init; }
    public int ContactPersonId { get; init; }
    public bool IsProformaInvoice { get; init; } = false;
    public int BankInformationId { get; init;}
}

public class CommercialInvoiceHandler : IRequestHandler<CommercialInvoice, CommercialInvoiceDto2>
{

    private readonly IAppDbContext _context;
    private readonly DocumentationService _documentationService;

    public CommercialInvoiceHandler(IAppDbContext context, DocumentationService documentationService)
    {
        _context = context;
        _documentationService = documentationService;
    }

    public async Task<CommercialInvoiceDto2> Handle(CommercialInvoice request, CancellationToken cancellationToken)
    {

        var op = _context.Operations
            .Where(d => d.Id == request.operationId).FirstOrDefault();
        if (op == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }
        if (request.PINumber != null)
        {
            op.PINumber = request.PINumber;
            op.PIDate = request.PIDate;
            _context.Operations.Update(op);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return (CommercialInvoiceDto2)await _documentationService
                    .GetDocumentation(request.IsProformaInvoice ? Documents.ProformaInvoice : Documents.CommercialInvoice, request.operationId, request.TruckAssignmentId, request.ContactPersonId, cancellationToken, request.BankInformationId);

    }
}