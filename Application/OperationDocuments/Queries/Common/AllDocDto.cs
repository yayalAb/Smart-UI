namespace Application.OperationDocuments.Queries.Common;
public class AllDocDto
{
    public string? OperationNumber { get; set; }
    public DateTime Date { get; set; }
    public string? PINumber { get; set; }
    public DateTime? PIDate { get; set; }
    public string? CountryOfOrigin { get; set; }
    public string? PurchaseOrderNumber { get; set; }
    public DateTime? PurchaseOrderDate { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerTinNumber { get; set; }
    public string? PortOfLoading { get; set; }
    public string? FinalDestination { get; set; }
    public string? TransportationMethod { get; set; }
    public string? Fright { get; set; }
    public string? PlaceOfDelivery { get; set; }
    public string? Consignee { get; set; }
    public string? PaymentTerm { get; set; }
    public string? PartialShipment { get; set; }
    public string? DriverName { get; set; }
    public string? DriverPhone { get; set; }
    public string AccountHolderName {get; set;}
    public string BankName {get; set;}
    public string AccountNumber {get; set;}
    public string SwiftCode {get; set;}
    public string BankAddress {get; set;}
    public IEnumerable<CIGoodsDto> Goods { get; set; } = null!;
}