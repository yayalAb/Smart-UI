using Domain.Common;
namespace Domain.Entities;

public class Container : BaseAuditableEntity
{

    public Container()
    {
        Goods = new HashSet<Good>();
    }
    public string ContianerNumber { get; set; } = null!;
    public float Size { get; set; }
    public string? Owner { get; set; }
    public int  LocationPortId { get; set; }
    public DateTime? ManufacturedDate { get; set; }
    public int OperationId { get; set; }    
    public int? TruckAssignmentId { get; set; }
    

    public virtual ICollection<Good> Goods { get; set; }
    public virtual Port LocationPort { get; set; }
    public virtual Operation Operation { get; set; } = null!;
    public virtual TruckAssignment TruckAssignment { get; set; } = null!;

}