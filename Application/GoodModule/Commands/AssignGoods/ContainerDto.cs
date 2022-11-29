using Application.Common.Mappings;
using Domain.Entities;
using Application.BillOfLoadingModule;

namespace Application.GoodModule.Commands.AssignGoodsCommand;

public class ContainerDto : IMapFrom<Container>
{
    public string ContianerNumber { get; set; } = null!;
    public string SealNumber { get; set; } = null!;
    public float Size { get; set; }
    public string Location { get; set; }
    public int? LocationPortId { get; set; }
    public List<GoodDto> Goods { get; set; }

}