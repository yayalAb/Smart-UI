using System.Reflection.Metadata;

namespace Domain.Entities;

public class Image
{

    public Image() {
        Drivers = new HashSet<Driver>();
        ShippingAgents = new HashSet<ShippingAgent>();
        Trucks = new HashSet<Truck>();
    }

    public int Id { get; set; }
    public byte[] Image1 { get; set; } = null!;
    
    public virtual ICollection<Driver> Drivers { get; set; }
    public virtual ICollection<ShippingAgent> ShippingAgents { get; set; }
    public virtual ICollection<Truck> Trucks { get; set; }

}