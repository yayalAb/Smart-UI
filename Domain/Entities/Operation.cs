using Domain.Common;
namespace Domain.Entities;

public class Operation : BaseAuditableEntity {

    public string? NameOnPermit { get; set; }
    public string? Consignee { get; set; }
    public string? NotifyParty { get; set; }
    public string? BillNumber { get; set; }
    public string? ShippingLine { get; set; }
    public string? GoodsDescription { get; set; }
    public float Quantity { get; set; }
    public float? GrossWeight { get; set; }
    public string? ATA { get; set; }
    public string? FZIN { get; set; }
    public string? FZOUT { get; set; }
    public string? DestinationType { get; set; }
    public byte[]? SourceDocument { get; set; }
    public DateTime? ActualDateOfDeparture { get; set; }
    public DateTime? EstimatedTimeOfArrival { get; set; }
    public string? VoyageNumber { get; set; }
    public string? TypeOfMerchandise { get; set; }
    public string OperationNumber { get; set; } = null!;
    public DateTime OpenedDate { get; set; }
    public string Status { get; set; } = null!;
    public byte[]? ECDDocument { get; set; }
    public int? ShippingAgentId { get; set; }
    public int? PortOfLoadingId { get; set; }
    public int CompanyId { get; set; }
    // has one
    public virtual Port PortOfLoading { get; set; } = null!;    
    public virtual ShippingAgent? ShippingAgent { get; set; } = null!;
    public virtual Company Company { get; set; } = null!;
    //has many
    public virtual ICollection<Payment> Payments { get; set; }
    public virtual ICollection<Container>? Containers { get; set; }
    public virtual ICollection<Documentation>? Documentaions { get; set; }
    public virtual ICollection<OperationStatus>? OperationStatuses { get; set; }
    public virtual ICollection<TruckAssignment>? TruckAssignments { get; set; }



}