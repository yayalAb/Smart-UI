namespace Domain.Entities;

public class TerminalPortFee {
    
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string? BankCode { get; set; }
    public float Amount { get; set; }
    public string Currency { get; set; } = null!;
    public string? Description { get; set; }
    public int OperationId { get; set; }

    public virtual Operation Operation { get; set; }

}