using Domain.Common;
namespace Domain.Entities;

public class Truck : BaseAuditableEntity {

    public Truck() {
        Operations = new HashSet<Operation>();
        Drivers = new HashSet<Driver>();
    }

    public string TruckNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public float? Capacity { get; set; }
    public byte[]? Image { get; set; }
    

    
    //has many
    public ICollection<Operation> Operations { get; set; }
    public ICollection<Driver> Drivers {get; set;}

}