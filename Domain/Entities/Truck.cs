using Domain.Common;
namespace Domain.Entities;

public class Truck : BaseAuditableEntity
{


    public string TruckNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string PlateNumber { get; set; } = null!;
    public float? Capacity { get; set; }
    public string? Image { get; set; }
    public bool IsAssigned { get; set; } = false;




    //has many
    public ICollection<TruckAssignment> TruckAssignments { get; set; }

}