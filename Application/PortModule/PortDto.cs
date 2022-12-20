using Application.Common.Mappings;
using Domain.Entities;

namespace Application.PortModule;
public class PortDto: IMapFrom<Port>
{
    public int Id { get; set; }
    public string PortNumber { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? Vollume { get; set; }
}