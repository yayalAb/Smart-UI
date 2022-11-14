using Domain.Common;
namespace Domain.Entities;

public class Truck : BaseAuditableEntity {


    public string TruckNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public float? Capacity { get; set; }
    public byte[]? Image { get; set; }
    

    
    //has many
    public ICollection<TruckAssignment> TruckAssignments {get; set;}

}