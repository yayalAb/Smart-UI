using Domain.Common;
namespace Domain.Entities;

public class Port : BaseAuditableEntity {

    public Port() {
        BillOfLoadings = new HashSet<BillOfLoading>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? Vollume { get; set; }

    public virtual ICollection<BillOfLoading> BillOfLoadings { get; set; }
    
}