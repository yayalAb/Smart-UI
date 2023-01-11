using Application.Common.Mappings;
using Domain.Entities;


namespace Application.OperationModule.Queries.oprationDetail
{
    public class oprationDetailDto : IMapFrom<Operation>
    {
    public int Id { get; set; }
       public string? Consignee { get; set; }
    public string? NotifyParty { get; set; }
    public string BillNumber { get; set; }
    public string? ShippingLine { get; set; }
    public string? GoodsDescription { get; set; }
    public float Quantity { get; set; } = 0;
    public float GrossWeight { get; set; } = 0;
    public string? ATA { get; set; }
    public string? FZIN { get; set; }
    public string? FZOUT { get; set; }
    public string DestinationType { get; set; }
    public string? SourceDocument { get; set; }
    public DateTime? ActualDateOfDeparture { get; set; }
    public DateTime? EstimatedTimeOfArrival { get; set; }
    public string? VoyageNumber { get; set; }
    public string OperationNumber { get; set; } = null!;
    public DateTime OpenedDate { get; set; }
    public string Status { get; set; } = null!;
    public string? ECDDocument { get; set; }
    public int? ShippingAgentId { get; set; }
    public int? PortOfLoadingId { get; set; }
    public int CompanyId { get; set; }
    /////------------Additionals------
    public string? SNumber { get; set; } // operation
    public DateTime? SDate { get; set; } //operation
    public string? RecepientName { get; set; }
    public string? VesselName { get; set; } // operation
    public DateTime? ArrivalDate { get; set; } // operation
    public string? CountryOfOrigin { get; set; } // operation
    public float REGTax { get; set; } = 0;
    public string? BillOfLoadingNumber { get; set; }
    public string? PINumber { get; set; }
    public DateTime? PIDate { get; set; }
    public string? FinalDestination { get; set; }//////////////////////////??////////////
    public string? Localization { get; set; }
    public string? Shipper { get; set; }
    public int ContactPersonId { get; set; }
    //--------------------------------------//
    // has one
    public virtual ContactPerson ContactPerson { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;
     public virtual ICollection<Container>? Containers { get; set; }
    public virtual ICollection<Good>? Goods { get; set; }
    public virtual ICollection<TruckAssignment>? TruckAssignments { get; set; }
  
  
    }
}