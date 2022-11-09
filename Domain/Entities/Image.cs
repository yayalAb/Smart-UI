using Domain.Common;

namespace Domain.Entities;

public class Image : BaseAuditableEntity
{

    public int Id { get; set; }
    public byte[] ImageData { get; set; } = null!;
    
    public virtual Driver Driver { get; set; } = null!;
    public virtual ShippingAgent ShippingAgent { get; set; } = null!;
    public virtual Truck Truck { get; set; } = null!;
    public virtual Container Container { get; set; } = null!;

}