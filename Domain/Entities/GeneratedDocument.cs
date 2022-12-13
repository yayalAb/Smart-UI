using Domain.Common;

namespace Domain.Entities;
public class GeneratedDocument : BaseAuditableEntity
{
    public string LoadType { get; set; } // container , unstaff
    public string DocumentType { get; set; } /// t1 , no.1....
    public int OperationId { get; set; }
    public int ExitPortId { get; set; }
    public int DestinationPortId { get; set; }
    public int ContactPersonId { get; set; }
    // has one
    public virtual Operation Operation { get; set; }
    public virtual Port ExitPort { get; set; }
    public virtual Port DestinationPort { get; set; }
    public virtual ContactPerson ContactPerson { get; set; }
    public virtual ICollection<Container> Containers { get; set; }
}