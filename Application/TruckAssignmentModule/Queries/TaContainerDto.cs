
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries
{
    public class TaContainerDto : IMapFrom<Container>
    {
        public int Id { get; set; }
        public string ContianerNumber { get; set; } = null!;

    }
}