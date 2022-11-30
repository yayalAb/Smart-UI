using Application.Common.Mappings;
using Domain.Entities;
using Application.GoodModule.Queries;

namespace Application.ContainerModule;

public class ContainerDto : IMapFrom<Container>
{
    public int? Id { get; set; }
    public string ContianerNumber { get; set; }
    public string SealNumber { get; set; }
    public float Size { get; set; }
    public string? Owner { get; set; }
    public string Location { get; set; }
    public int? LocationPortId { get; set; }
    public DateTime? ManufacturedDate { get; set; }
    public int? OperationId { get; set; }
    public int? TruckAssignmentId { get; set; }

    public List<FetchGoodDto>? Goods { get; set; }
}