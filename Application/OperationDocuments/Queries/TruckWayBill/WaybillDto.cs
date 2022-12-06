using Application.OperationDocuments.Queries.Common;

namespace Application.OperationDocuments.Queries.TruckWayBill;
public class WaybillDto : DocsDto
{
    //TODO: what is declaration number
    public string? DeclarationNumber { get; set; }
    public string? DriverName { get; set; }
    public string? DriverPhone { get; set; }
    
}