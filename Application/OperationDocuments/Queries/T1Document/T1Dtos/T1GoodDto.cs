using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.T1Document.T1Dtos;

public class T1GoodDto : IMapFrom<Good> {
    public string? HSCode { get; set; }
    public float Weight { get; set; }
    public float? Quantity { get; set; }
    public string? ChasisNumber {get; set; }
    public string? Unit { get; set; } 
    public float? UnitPrice { get; set; }
}