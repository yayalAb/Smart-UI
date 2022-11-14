using Application.Common.Mappings;
using Domain.Entities;
using Application.BillOfLoadingModule;

namespace Application.ContainerModule;

public class ContainerDto : IMapFrom<Container> {
    public string ContianerNumber { get; set; } = null!;
    public float Size { get; set; }
    public string? Owner { get; set; }
    public string? Location { get; set; }
    public DateTime? ManufacturedDate { get; set; }
    public byte[]? Image { get; set; }
    
    public virtual ICollection<BillOfLoadingDto> BillOfLoadings { get; set; }
    public virtual ICollection<Good> Goods { get; set; }
}