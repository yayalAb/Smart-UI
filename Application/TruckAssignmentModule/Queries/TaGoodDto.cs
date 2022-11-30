using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries
{
    public class TaGoodDto : IMapFrom<Good>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}