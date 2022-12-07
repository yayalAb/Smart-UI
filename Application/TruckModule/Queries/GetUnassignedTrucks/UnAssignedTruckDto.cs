using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TruckModule.Queries.GetUnassignedTrucks;
public class UnAssignedTruckDto : IMapFrom<Truck>
{
    public int Id { get; set; }
    public string TruckNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public float? Capacity { get; set; }
    public bool IsAssigned { get; set; }
    public string PlateNumber { get; set; }
}