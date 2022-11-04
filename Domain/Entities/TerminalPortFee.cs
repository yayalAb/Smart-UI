namespace Domain.Entities;

public class TerminalPortFee
{
    public int Id { get; set; }
    public string type { get; set; }
    public DateTime paymentDate { get; set; }
    public string paymentMethod { get; set; }
    public string bankCode { get; set; }
    public float amount { get; set; }
    public string currency { get; set; }
    public string description { get; set; }
    public int operationId { get; set; }
}