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
}