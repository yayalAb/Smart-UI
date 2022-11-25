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
    public string SealNumber {get; set;} =null!;
    public string Location {get; set;} =null!;
    public int?  LocationPortId { get; set; }
    public bool IsAssigned { get; set; } = false;
    public int OperationId { get; set; }    
    public int? TruckAssignmentId { get; set; }

    

    public virtual ICollection<Good> Goods { get; set; }
    public virtual Port? LocationPort { get; set; }
    public virtual Operation Operation { get; set; } = null!;
    public virtual ICollection< TruckAssignment> TruckAssignments { get; set; } = null!;

}