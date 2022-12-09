using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Gatepass.GPDtos;

public class GPOperationDto : IMapFrom<Operation> {
    public string? SNumber { get; set; }
    public string? Localization { get; set; }
}