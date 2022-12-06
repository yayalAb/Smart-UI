
namespace Application.ContainerModule.Queries.ContainerByOperation;

public class ContainerGoodCountDto {
    public int? Id { get; set; }
    public string ContianerNumber { get; set; } = null!;
    public string SealNumber { get; set; } = null!;
    public float Size { get; set; }
    public string Location { get; set; } = null!;
    public int? LocationPortId { get; set; }
    public int? OperationId { get; set; }
    public int? TruckAssignmentId { get; set; }
    public int GoodCount {get; set;}
}