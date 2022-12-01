using Application.Common.Mappings;
using Domain.Entities;

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
    public List<int>? ContainerIds { get; set; }
    public List<int>? GoodIds { get; set; }
}