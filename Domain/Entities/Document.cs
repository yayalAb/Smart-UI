using Domain.Common;

namespace Domain.Entities;

public class Document : BaseAuditableEntity {
    
    public string Type { get; set; }    
    public byte[] DocumentData { get; set; } = null!;

    public virtual Operation Operation { get; set; } = null!;
    public virtual BillOfLoading BillOfLoading { get; set; } = null!;

}