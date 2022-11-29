using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries
{
    public class TaPortDto : IMapFrom<Port>
    {
        public int Id { get; set; }
        public string PortNumber { get; set; }
        public string Country { get; set; }
    }
}