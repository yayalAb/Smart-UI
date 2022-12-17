using Domain.Common;

namespace Domain.Entities;

public class Address : BaseAuditableEntity
{
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Subcity { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? POBOX { get; set; }

    public virtual Company Company { get; set; } = null!;
    public virtual Driver Driver { get; set; } = null!;
    public virtual ShippingAgent ShippingAgent { set; get; } = null!;

}