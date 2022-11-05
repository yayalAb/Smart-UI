namespace Domain.Entities;

public class Operation {
    public int Id { get; set; }
    public string operationNumber { get; set; }
    public DateTime openedDate { get; set; }
    public string status { get; set; }
    public string billOfLoadingId { get; set; }
    public int truckId { get; set; }
    public int companyId { get; set; }
    public int driverId { get; set; }
    
    public BillOfLoading billOfLoading { get; set; }
    public Truck operationTruck { get; set; }
    public Company company { get; set; }
    public Driver driver { get; set; }
    
    public ICollection<TerminalPortFee> terminalPortFee { get; set; } 
    public ICollection<ShippingAgentFee> shippingAgentFee { get; set; }
    public ICollection<Documentation> documentaion { get; set; }
}