using System.Reflection.Metadata;

namespace Domain.Entities;

public class ECDDocument
{
    public int Id { get; set; }
    public Blob document { get; set; }
    public int operationId { get; set; }
}