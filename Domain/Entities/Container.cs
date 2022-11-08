namespace Domain.Entities;

public class Container
{

    public Container()
    {
        BillOfLoadings = new HashSet<BillOfLoading>();
        Goods = new HashSet<Good>();
    }

    public int Id { get; set; }
    public string ContianerNumber { get; set; } = null!;
    public float Size { get; set; }
    public string? Owner { get; set; }
    public string? Loacation { get; set; }
    public DateTime? ManufacturedDate { get; set; }
    public int AddressId { get; set; }
    
    public virtual Address Address { set; get; } = null!;
    
    public virtual ICollection<BillOfLoading> BillOfLoadings { get; set; }
    public virtual ICollection<Good> Goods { get; set; }
}