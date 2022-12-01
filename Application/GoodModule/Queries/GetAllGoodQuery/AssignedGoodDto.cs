using Application.ContainerModule;

namespace Application.GoodModule.Queries.GetAllGoodQuery
{
    public class AssignedGoodDto : FetchGoodDto
    {
        public int OperationId { get; set; }
        public ICollection<ContainerDto>? Containers { get; set; }
        public ICollection<FetchGoodDto>? Goods { get; set; }
    }
}