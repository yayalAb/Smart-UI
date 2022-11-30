using System.ComponentModel;
using Application.Common.Mappings;

namespace Application.GoodModule.Commands.UpdateGoodCommand;

public class UpdateGoodContainerDto : IMapFrom<Container>
{
    public int? Id { get; set; }
    public string ContianerNumber { get; set; } = null!;
    public string SealNumber { get; set; } = null!;
    public float Size { get; set; }
    public string Location { get; set; }
    public int? LocationPortId { get; set; }
    public List<UpdateGoodDto> Goods { get; set; }

}