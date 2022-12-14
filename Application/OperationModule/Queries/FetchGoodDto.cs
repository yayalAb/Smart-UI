using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationModule.Queries;

public class FetchGoodDto : IMapFrom<Good>
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public float Weight { get; set; }
    public int Quantity { get; set; }
    public int NumberOfPackages { get; set; }
    public string Type { get; set; }
    public string? ChasisNumber { get; set; }
    public string? EngineNumber { get; set; }
    public string? ModelCode { get; set; }

    public bool IsAssigned { get; set; } = false;
    public int? ContainerId { get; set; }
    /////------------Additionals------
    public string? SNumber { get; set; } // operation
    public DateTime? SDate { get; set; } //operation
    public string? RecepientName { get; set; }
    public string? VesselName { get; set; } // operation
    public DateTime? ArrivalDate { get; set; } // operation
    public string? ConnaissementNumber { get; set; } // operation
    public string? CountryOfOrigin { get; set; } // operation
    public float? REGTax { get; set; }//
    //--------------------------------------//
}