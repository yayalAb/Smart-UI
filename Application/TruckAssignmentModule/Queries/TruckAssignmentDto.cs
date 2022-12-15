using Application.Common.Mappings;
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
    public string TransportationMethod { get; set; }
    public float AgreedTariff { get; set; }
    public string Currency { get; set; }
    public string SENumber { get; set; }
    public DateTime? Date { get; set; }
    public string GatePassType { get; set; }
    public TaPortDto SourcePort { get; set; }
    public TaPortDto DestinationPort { get; set; }

    public virtual ICollection<TaContainerDto>? Containers { get; set; }
    public virtual ICollection<TaGoodDto>? Goods { get; set; }
}