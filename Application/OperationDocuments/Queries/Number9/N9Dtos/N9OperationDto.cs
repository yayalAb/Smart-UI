using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Number9.N9Dtos;

public class N9OperationDto : IMapFrom<Operation>
{
    public int Id { get; set; }
    public string? ShippingLine { get; set; }
    public string? GoodsDescription { get; set; }
    public float? Quantity { get; set; }
    public float? GrossWeight { get; set; }
    public string DestinationType { get; set; }
    public string? SourceDocument { get; set; }
    public DateTime? EstimatedTimeOfArrival { get; set; }
    public string? VoyageNumber { get; set; }
    public string? TypeOfMerchandise { get; set; }
    public string OperationNumber { get; set; } = null!;
    public int CompanyId { get; set; }
    /////------------Additionals------
    public string? SNumber { get; set; } // operation
    public DateTime? SDate { get; set; } //operation
    public string? VesselName { get; set; } // operation
    public DateTime? ArrivalDate { get; set; } // operation
    public string? CountryOfOrigin { get; set; } // operation
    public float? REGTax { get; set; }//
    public string? FinalDestination { get; set; }
    public string? Localization { get; set; }
    public virtual N9PortOfLoadingDto PortOfLoading { get; set; }
    public N9CompanyDto Company { get; set; }
    public ICollection<N9GoodDto>? Goods { get; set; }
}