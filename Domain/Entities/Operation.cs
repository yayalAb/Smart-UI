using Domain.Common;
namespace Domain.Entities;

public class Operation : BaseAuditableEntity {

    public Operation() {
        ECDDocuments = new HashSet<ECDDocument>();
    }
    public string OperationNumber { get; set; } = null!;
    public DateTime OpenedDate { get; set; }
    public string Status { get; set; } = null!;
    public int BillOfLoadingId { get; set; }
    public int? TruckId { get; set; }
    public int? CompanyId { get; set; }
    public int? DriverId { get; set; }
    
    public virtual BillOfLoading BillOfLoading { get; set; } = null!;
    public virtual Company? Company { get; set; }
    public virtual Driver? Driver { get; set; }
    public virtual Truck? Truck { get; set; }
    public TerminalPortFee TerminalPortFee { get; set; } 
    public ShippingAgentFee ShippingAgentFee { get; set; }
    
    public Documentation Documentaion { get; set; }
    public virtual ICollection<ECDDocument> ECDDocuments { get; set; }

}