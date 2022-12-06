
namespace Application.OperationDocuments.Queries.CommercialInvoice;

public class CommercialInvoiceDto2 {
    public string OperationNumber { get; set; }
    public DateTime Date { get; set; }
    public string? PINumber { get; set; }
    public DateTime? PIDate { get; set; }
    public string? PurchaseOrderNumber { get; set; }
    public DateTime? PurchaseOrderDate { get; set;}
    public string? CustomerName { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerTinNumber { get; set;}
    public string? PortOfLoading { get; set; }
    public string? FinalDestination { get; set; }
    public string? TransportationMethod { get; set; }
    public string? PaymentTerm { get; set; }
    public string? PartialShipment { get; set; }
    public IEnumerable<CIGoodsDto> Goods { get; set; }
                            

}