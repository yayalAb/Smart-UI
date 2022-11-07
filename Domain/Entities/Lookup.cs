using Domain.Common;

namespace Domain.Entities;

public class Lookup : BaseAuditableEntity
{
    public string Type { get; set; }
    public string Name { get; set; }
}