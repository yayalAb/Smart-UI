namespace Domain.Entities;

public class Address
{

    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Subcity { get; set; }
    public string? Country { get; set; }
    public string POBOX { get; set; }

    public virtual Company Company { get; set; } = null!;
    public virtual Container Container { get; set; } = null!;
    public virtual Driver Driver { get; set; } = null!;
    public virtual ShippingAgent ShippingAgent { set; get; } = null!;
    
}