using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Number9.N9Dtos;

public class N9GoodDto : IMapFrom<Good>
{
    public string Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public float Weight { get; set; }
    public float? Quantity { get; set; }
    public int RemainingQuantity { get; set; }
    public string? ChasisNumber { get; set; }
    public string? EngineNumber { get; set; }
    public string? ModelCode { get; set; }
    // additionals 
    public string? Unit { get; set; }
    public float? UnitPrice { get; set; }
    public float? CBM { get; set; }
}