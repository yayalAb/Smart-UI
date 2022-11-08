using Domain.Common;
namespace Domain.Entities;

public class Lookup : BaseAuditableEntity
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public string Name { get; set; } = null!;
}