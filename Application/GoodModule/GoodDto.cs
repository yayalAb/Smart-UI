using Application.Common.Mappings;
using Domain.Entities;
using Application.DriverModule;

namespace Application.GoodModule;

public class GoodDto 
{
    public int? Id {get; set; }
    public string Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public float Weight { get; set; }
    public float? Quantity { get; set; }
    public int NumberOfPackages { get; set; }
    public string Type { get; set; }
    public string? Location {get; set;}
    public string? ChasisNumber { get; set; }
    public string? EngineNumber { get; set; }
    public string? ModelCode { get; set; }
    public int? LocationPortId { get; set; }
}