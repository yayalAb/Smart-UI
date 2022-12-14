using Domain.Common;
namespace Domain.Entities;

public class Good : BaseAuditableEntity
{
    public string Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public float Weight { get; set; }
    public string WeightUnit { get; set; }
    public int Quantity { get; set; }
    public int RemainingQuantity {get; set;}
    public string Type {get; set;} 
    public string Location {get; set;}
    public string? ChasisNumber {get; set; }
    public string? EngineNumber {get; set;}
    public string? ModelCode { get; set; }
    public bool IsAssigned { get; set; } = false;
    // additionals 
    public string Unit { get; set; } 
    public float UnitPrice { get; set; }
    public float? CBM { get; set; }
    //////
    public int? ContainerId { get; set; }
    public int OperationId {get; set; }
    public int? LocationPortId { get; set; }
    
    public virtual Container? Container { get; set; } = null!; 
    public virtual Operation Operation { get; set; } = null!;
    public virtual Port? LocationPort { get; set; }
    public virtual ICollection<TruckAssignment> TruckAssignments { get; set; } =  null!;
    public virtual ICollection<GeneratedDocumentGood>? DocumentGoods {get; set;}

}