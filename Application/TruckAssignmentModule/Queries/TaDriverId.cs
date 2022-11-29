using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries
{
    public class TaDriverDto : IMapFrom<Driver>
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
    }
}