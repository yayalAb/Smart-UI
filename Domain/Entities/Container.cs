using Domain.Common;
namespace Domain.Entities;

public class Container : BaseAuditableEntity
{

    public Container()
    {
        Goods = new HashSet<Good>();
        TruckAssignments = new HashSet<TruckAssignment>();
    }
    public string ContianerNumber { get; set; } = null!;
    public string? GoodsDescription { get; set; }
    public string SealNumber {get; set;} =null!;
    public string Location {get; set;} =null!;
    public int Article { get; set; } = 1;
    public string Size { get; set; } 
    public float GrossWeight { get; set; }
    public string WeightMeasurement { get; set; }
    public int Quantity { get; set; }
    public float TotalPrice { get; set; }
    public string Currency { get; set; }
    public int?  LocationPortId { get; set; }
    public bool IsAssigned { get; set; } = false;
    public int OperationId { get; set; } 

    

    public virtual ICollection<Good> Goods { get; set; }
    public virtual Port? LocationPort { get; set; }
    public virtual Operation Operation { get; set; } = null!;
    public virtual ICollection< TruckAssignment> TruckAssignments { get; set; } = null!;

}