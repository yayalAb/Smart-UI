
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.T1Document.T1Dtos;

public class T1OperationDto : IMapFrom<Operation> {
    public string? SNumber {get; set;}
    public string? Localization { get; set; }
    public string? GoodsDescription { get; set; }
}