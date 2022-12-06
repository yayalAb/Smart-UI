
using Application.OperationDocuments.Queries.Common;

namespace Application.OperationDocuments.Queries.CommercialInvoice;

public class CommercialInvoiceDto2 : DocsDto
{
    public string? PaymentTerm { get; set; }
    public string? PartialShipment { get; set; }
    public string AccountHolderName {get; set;}
    public string BankName {get; set;}
    public string AccountNumber {get; set;}
    public string SwiftCode {get; set;}
    public string BankAddress {get; set;}
}