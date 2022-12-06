using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DocumentationModule.Queries.GetDocumentationPaginatedList;
public class DocumentationDto : IMapFrom<Documentation>
{
    public int Id {get; set;}
    public int OperationId { get; set; }
    public string Type { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? BankPermit { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? Source { get; set; }
    public string? Destination { get; set; }

    public string? PurchaseOrderNumber { get; set; }
    public DateTime PurchaseOrderDate { get; set; }
    public string? PaymentTerm { get; set; }
    public bool? IsPartialShipmentAllowed { get; set; }
    public string? Fright { get; set; }
    
 
}