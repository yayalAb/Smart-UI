using Application.OperationDocuments.Queries.Common;

namespace Application.OperationDocuments.Queries.TruckWayBill;
public class WaybillDto : DocsDto
{
    public string? DeclarationNumber { get; set; }
    public string? DriverName { get; set; }
    public string? DriverPhone { get; set; }
    public string? DriverLicenceNumber { get; set; }
    public string? TruckNumber { get; set; }
    public string? PlateNumber { get; set; }

}