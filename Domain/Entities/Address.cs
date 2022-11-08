using Domain.Common;

namespace Domain.Entities;

public class Address : BaseAuditableEntity
{
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Subcity { get; set; }
    public string? Country { get; set; }
    public string? POBOX { get; set; }
}