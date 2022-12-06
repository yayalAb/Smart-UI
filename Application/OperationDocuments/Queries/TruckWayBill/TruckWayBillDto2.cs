using Application.OperationDocuments.Queries.Common;

namespace Application.OperationDocuments.Queries.TruckWayBill;
public class TruckWayBillDto2 : DocsDto
{
    public string? Fright { get; set; }
    public string? PlaceOfDelivery { get; set; }
    public string? Consignee { get; set; }
    
}