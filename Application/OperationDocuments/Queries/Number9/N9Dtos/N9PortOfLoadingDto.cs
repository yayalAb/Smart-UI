using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Number9.N9Dtos;

public class N9PortOfLoadingDto : IMapFrom<Port> {
    public string PortNumber { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? Vollume { get; set; }
}