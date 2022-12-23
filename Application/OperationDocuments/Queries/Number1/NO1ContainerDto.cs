using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Number1;
public class No1ContainerDto : IMapFrom<Container> {
    public string? ContianerNumber { get; set; }
    public string? SealNumber { get; set; }
    public int Article { get; set; } = 0;
    public float GrossWeight { get; set; } = 0;
    public string WeightMeasurement { get; set; }
    public int Quantity { get; set; } = 0;
    public float TotalPrice { get; set; } = 0;
    public string Currency { get; set; }
    public DateTime Created {get; set;}
}