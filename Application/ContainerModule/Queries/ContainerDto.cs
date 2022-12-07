using Application.Common.Mappings;
using Domain.Entities;
using Application.GoodModule.Queries;

namespace Application.ContainerModule;

public class ContainerDto : IMapFrom<Container>
{
    public int? Id { get; set; }
    public string ContianerNumber { get; set; } = null!;
    public string SealNumber { get; set; } = null!;
    public float Size { get; set; }
    public string Location { get; set; } = null!;
    public int? LocationPortId { get; set; }
    public int? OperationId { get; set; }

    public List<FetchGoodDto>? Goods { get; set; }
}