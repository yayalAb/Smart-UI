using Domain.Common;

namespace Domain.Entities;

public class ECDDocument : BaseAuditableEntity {
    
    public byte[] Document { get; set; } = null!;
    public int OperationId { get; set; }

    public virtual Operation Operation { get; set; } = null!;

}