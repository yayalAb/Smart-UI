using Domain.Common;
namespace Domain.Entities;

public class Driver : BaseAuditableEntity {

    public Driver() {
        Operations = new HashSet<Operation>();
    }

    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string LicenceNumber { get; set; } = null!;
    public int AddressId { get; set; }
    public int ImageId { get; set; }
    public int? TruckId { get; set; }
    
    //has one
    public virtual Address Address { get; set; }
    public virtual Truck Truck { get; set; }
    public virtual Image Image { get; set; } = null!;
    
    //has many
    public virtual ICollection<Operation> Operations { get; set; }

}