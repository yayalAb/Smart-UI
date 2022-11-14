using Domain.Common;

namespace Domain.Entities;

public class Lookup : BaseAuditableEntity
{
    public string Key { get; set; }
    public string Value { get; set; }
}