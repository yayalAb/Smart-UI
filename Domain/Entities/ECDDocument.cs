using System.Reflection.Metadata;

namespace Domain.Entities;

public class ECDDocument {
    
    public int Id { get; set; }
    public byte[] Document { get; set; } = null!;
    public int OperationId { get; set; }

    public virtual Operation Operation { get; set; } = null!;

}