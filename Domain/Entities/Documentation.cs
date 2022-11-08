using Domain.Common;
namespace Domain.Entities;

public class Documentation : BaseAuditableEntity
{
    public int Id { get; set; }
    public int OperationId { get; set; }
    public string Type { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? BankPermit { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? ImporterName { get; set; }
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? TinNumber { get; set; }
    public string? TransportationMethod { get; set; }
    public string? Source { get; set; }
    public string? Destination { get; set; }
    
    public virtual Operation Operation { get; set; } = null!;
}