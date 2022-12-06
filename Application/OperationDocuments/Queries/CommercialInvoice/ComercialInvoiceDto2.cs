
using Application.OperationDocuments.Queries.Common;

namespace Application.OperationDocuments.Queries.CommercialInvoice;

public class CommercialInvoiceDto2 : DocsDto
{
    public string? PaymentTerm { get; set; }
    public string? PartialShipment { get; set; }
}