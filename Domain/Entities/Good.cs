using Domain.Common;
namespace Domain.Entities;

public class Good : BaseAuditableEntity
{
    public int Id {get; set;}
    public string Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public float Weight { get; set; }
    public float? Quantity { get; set; }
    public int NumberOfPackages {get; set;}
    public string Type {get; set;} 
    public string Location {get; set;}
    public string? ChasisNumber {get; set; }
    public string? EngineNumber {get; set;}
    public string? ModelCode { get; set; }
    public bool IsAssigned { get; set; } = false;
    public int? ContainerId { get; set; }
    public int OperationId {get; set; }
    public int TruckAssignmentId { get; set; }
    
    public virtual Container Container { get; set; } = null!; 
    public virtual Operation Operation { get; set; } = null!;
    public virtual ICollection<TruckAssignment> TruckAssignments { get; set; } =  null!;

}