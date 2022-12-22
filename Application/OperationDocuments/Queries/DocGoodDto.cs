using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries;
public class DocGoodDto : IMapFrom<Good>
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public float Weight { get; set; }
    public string WeightUnit { get; set; }
    public int Quantity { get; set; }
    public int InitialQuantity { get;set; }=0;
    public int RemainingQuantity { get; set; }
    public string? Type { get; set; }
    public string? Location { get; set; }
    public string? ChasisNumber { get; set; }
    public string? EngineNumber { get; set; }
    public string? ModelCode { get; set; }
    public bool IsAssigned { get; set; } = false;
    // additionals 
    public string? Unit { get; set; }
    public float? UnitPrice { get; set; }
    public float? CBM { get; set; }
    //////
}