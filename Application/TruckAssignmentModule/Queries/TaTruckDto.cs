using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries
{
    public class TaTruckDto : IMapFrom<Truck>
    {
        public int Id { get; set; }
        public string TruckNumber { get; set; }
    }
}