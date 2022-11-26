using Application.Common.Mappings;
using Application.ContainerModule;
using Application.GoodModule;
using Application.GoodModule.Queries;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries;
public class TruckAssignmentDto : IMapFrom<TruckAssignment>
{
    public int Id { get; set; }
    public int DriverId { get; set; }
    public int TruckId { get; set; }
    public int OperationId { get; set; }
    public int SourcePortId { get; set; }
    public int DestinationPortId { get; set; }
    public virtual ICollection<ContainerDto>? Containers { get; set; }
    public virtual ICollection<FetchGoodDto>? Goods { get; set; }
}