using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TruckAssignmentModule.Queries
{
    public class TaOperaitonDto : IMapFrom<Operation>
    {
        public int Id { get; set; }
        public string OperationNumber { get; set; }
    }
}