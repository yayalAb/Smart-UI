
namespace Application.ContainerModule.Queries.ContainerByOperation;

public class ContainerGoodCountDto
{
    public int? Id { get; set; }
    public string ContianerNumber { get; set; } = null!;
    public string SealNumber { get; set; } = null!;
    public string Size { get; set; }
    public int Article { get; set; }
    public float TotalPrice { get; set; }
    public string Currency { get; set; }
    public float GrossWeight { get; set; }
    public string WeightMeasurement { get; set; }
    public string Location { get; set; } = null!;
    public int? LocationPortId { get; set; }
    public int? OperationId { get; set; }
    public int GoodCount { get; set; }
}