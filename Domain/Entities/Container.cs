using Domain.Common;
namespace Domain.Entities;

public class Container : BaseAuditableEntity
{

    public Container()
    {
        BillOfLoadings = new HashSet<BillOfLoading>();
        Goods = new HashSet<Good>();
    }
    public string ContianerNumber { get; set; } = null!;
    public float Size { get; set; }
    public string? Owner { get; set; }
    public string? Location { get; set; }
    public DateTime? ManufacturedDate { get; set; }
    public byte[]? Image { get; set; } 
    
    public virtual ICollection<BillOfLoading> BillOfLoadings { get; set; }
    public virtual ICollection<Good> Goods { get; set; }
}