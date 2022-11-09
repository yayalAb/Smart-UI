using Domain.Common;
namespace Domain.Entities;

public class Truck : BaseAuditableEntity {

    public Truck() {
        Operations = new HashSet<Operation>();
        Drivers = new HashSet<Driver>();
    }

    public int Id { get; set; }
    public string TruckNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public float Capacity { get; set; }
    public int ImageId { get; set; }
    
    //has one
    public Image Image { get; set; } = null!;
    
    //has many
    public ICollection<Operation> Operations { get; set; }
    public ICollection<Driver> Drivers {get; set;}

}