using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Gatepass.GPDtos;

public class GPGoodDto : IMapFrom<Good>
{
    public float? Weight { get; set; }
    public float? Quantity { get; set; }
    public string? ContainerNumber { get; set; }

}