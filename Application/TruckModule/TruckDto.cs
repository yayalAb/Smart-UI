using Application.Common.Mappings;
using Domain.Entities;
using Application.DriverModule;

namespace Application.TruckModule;

public class TruckDto : IMapFrom<Truck> {
    public int Id {get; set;}
    public string TruckNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public float? Capacity { get; set; }
    public byte[]? Image { get; set; }
    public bool IsAssigned { get; set; }


    public ICollection<DriverDto> Drivers {get; set;}
}