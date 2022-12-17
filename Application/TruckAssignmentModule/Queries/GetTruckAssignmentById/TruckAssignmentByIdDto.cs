namespace Application.TruckAssignmentModule.Queries.GetTruckAssignmentById;
public class TruckAssignmentByIdDto
{
    public int Id { get; set; }
    public int OperationId { get; set; }
    public int DriverId { get; set; }
    public int TruckId { get; set; }
    public string SourceLocation { get; set; }
    public string DestinationLocation { get; set; }
    public int? SourcePortId { get; set; }
    public int? DestinationPortId { get; set; }
    public string TransportationMethod { get; set; }
    public float AgreedTariff { get; set; }
    public string Currency { get; set; }
    public string SENumber { get; set; }
    public DateTime? Date { get; set; }
    public string GatePassType { get; set; }
    public List<int>? ContainerIds { get; set; }
    public List<int>? GoodIds { get; set; }
}