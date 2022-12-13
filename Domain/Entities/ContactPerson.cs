using Domain.Common;
namespace Domain.Entities;

public class ContactPerson : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string TinNumber { get; set; } = null!;
    public string Country { get; set; } = null!; 
    public string City { get; set; } = null!;
    public int CompanyId { get; set; }

    
    //has one
    public virtual Company Company { get; set; } = null!;
    public virtual Operation Operation { get; set; } = null!;
    //has many
    public virtual ICollection<GeneratedDocument> GeneratedDocuments {get; set; } = null!;
}