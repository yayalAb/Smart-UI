using Application.Common.Mappings;
using Domain.Entities;
using Application.DriverModule;

namespace Application.GoodModule;

public class GoodDto : IMapFrom<Good> {
    public int Id {get; set;}
    public string? Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public string? CBM { get; set; }
    public float? Weight { get; set; }
    public float? Quantity { get; set; }
    public float? UnitPrice { get; set; }
    public string? UnitOfMeasurnment { get; set; }
    public int ContainerId { get; set; }
}