using Application.Common.Mappings;
using Application.GoodModule.Queries;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries;
public class TruckAssignmentDto : IMapFrom<TruckAssignment>
{
    public int Id { get; set; }
    public TaDriverDto Driver { get; set; }
    public TaTruckDto Truck { get; set; }
    public TaOperaitonDto Operation { get; set; }
    public string SourceLocation { get; set; }
    public string DestinationLocation { get; set; }
    public TaPortDto SourcePort { get; set; }
    public TaPortDto DestinationPort { get; set; }

    public virtual ICollection<TaContainerDto>? Containers { get; set; }
    public virtual ICollection<TaGoodDto>? Goods { get; set; }
}