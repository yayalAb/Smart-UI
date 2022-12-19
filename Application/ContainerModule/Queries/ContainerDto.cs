using Application.Common.Mappings;
using Application.GoodModule.Queries;
using Domain.Entities;

namespace Application.ContainerModule;

public class ContainerDto : IMapFrom<Container>
{
    public int? Id { get; set; }
    public string ContianerNumber { get; set; } = null!;
    public string? GoodsDescription { get; set; }
    public string SealNumber { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int Article { get; set; } = 1;
    public string Size { get; set; }
    public float GrossWeight { get; set; } = 0;
    public string WeightMeasurement { get; set; }
    public int Quantity { get; set; } = 0;
    public float TotalPrice { get; set; } = 0;
    public string Currency { get; set; }
    public int? LocationPortId { get; set; }
    public bool IsAssigned { get; set; } = false;
    public int OperationId { get; set; }
    public int? GeneratedDocumentId { get; set; }
    public List<FetchGoodDto>? Goods { get; set; }
}