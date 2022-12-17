using Domain.Common;
namespace Domain.Entities;

public class Port : BaseAuditableEntity
{

    public string PortNumber { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? Vollume { get; set; }

    public virtual ICollection<Container> Containers { get; set; }
    public virtual ICollection<Good> Goods { get; set; }
    public virtual ICollection<TruckAssignment> DestinationPortTruckAssignments { get; set; }
    public virtual ICollection<TruckAssignment> SourcePortTruckAssignments { get; set; }
    public virtual ICollection<Operation> Operations { get; set; }
    public virtual ICollection<GeneratedDocument> ExitPortGeneratedDocuments { get; set; }
    public virtual ICollection<GeneratedDocument> DestinationPortGeneratedDocuments { get; set; }
}