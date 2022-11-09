using Domain.Common;
namespace Domain.Entities;

public class BillOfLoading : BaseAuditableEntity {
    
    public string CustomerName { get; set; }
    public string? NameOnPermit { get; set; }
    public string? Consignee { get; set; }
    public string NotifyParty { get; set; }
    public string BillNumber { get; set; }
    public string ShippingLine { get; set; }
    public string GoodsDescription { get; set; }
    public float Quantity { get; set; }
    public int ContainerId { get; set; }
    public float? GrossWeight { get; set; }
    public string? ATA { get; set; }
    public string? FZIN { get; set; }
    public string? FZOUT { get; set; }
    public string DestinationType { get; set; }
    public int ShippingAgentId { get; set; }
    public int PortOfLoadingId { get; set; }
    public DateTime? ActualDateOfDeparture { get; set; }
    public DateTime? EstimatedTimeOfArrival { get; set; }
    public string VoyageNumber { get; set; }
    public string TypeOfMerchandise { get; set; }
    public int? BillOfLoadingDocumentId { get; set; }
    public virtual Container Container { get; set; } = null!;
    public virtual Port Port { get; set; } = null!;
    public virtual Operation Operation { get; set; } = null!;
    public virtual Document BillOfLoadingDocument { get; set; } = null!;    
    public virtual ShippingAgent ShippingAgent { get; set; } = null!;   

}