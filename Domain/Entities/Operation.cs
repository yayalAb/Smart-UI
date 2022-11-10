using Domain.Common;
namespace Domain.Entities;

public class Operation : BaseAuditableEntity {

    public string OperationNumber { get; set; } = null!;
    public DateTime OpenedDate { get; set; }
    public string Status { get; set; } = null!;
    public int BillOfLoadingId { get; set; }
    public int? TruckId { get; set; }
    public int? CompanyId { get; set; }
    public int? DriverId { get; set; }
    public int? ECDDocumentId { get; set; }
    
    public virtual BillOfLoading BillOfLoading { get; set; } = null!;
    public virtual Company? Company { get; set; }
    public virtual Driver? Driver { get; set; }
    public virtual Truck? Truck { get; set; }
    public virtual ICollection<TerminalPortFee>? TerminalPortFees { get; set; } 
    public virtual ICollection<ShippingAgentFee>? ShippingAgentFees { get; set; }
    public virtual Document? ECDDocument { get; set; }
    public virtual ICollection<Documentation>? Documentaions { get; set; }

}