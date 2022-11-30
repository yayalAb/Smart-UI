using System.Reflection.Metadata;
using Domain.Common;
namespace Domain.Entities;

public class Driver : BaseAuditableEntity {

    public string Fullname { get; set; } = null!;
    public string LicenceNumber { get; set; } = null!;
    public int AddressId { get; set; }
    public string? Image { get; set; }
    public bool IsAssigned { get; set; } = false;
    
    //has one
    public virtual Address Address { get; set; }
    
    //has many
    public virtual ICollection<TruckAssignment> TruckAssignments { get; set; } 
  

}