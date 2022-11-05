namespace Domain.Entities;

public class Documentation
{
    public int Id { get; set; }
    public int operationId { get; set; }
    public string type { get; set; }
    public DateTime date { get; set; }
    public string bankPermit { get; set; }
    public string invoiceNumber { get; set; }
    public string importerName { get; set; }
    public string phone { get; set; }
    public string country { get; set; }
    public string city { get; set; }
    public string tinNumber { get; set; }
    public string transportaionMethod { get; set; }
    public string source { get; set; }
    public string destination { get; set; }
    
    public Operation operation { get; set; }
}