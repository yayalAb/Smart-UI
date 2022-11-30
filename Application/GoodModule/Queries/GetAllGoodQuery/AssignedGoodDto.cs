using Application.ContainerModule;

namespace Application.GoodModule.Queries.GetAllGoodQuery
{
    public class AssignedGoodDto
    {
        public int OperationId { get; set; }
        public List<ContainerDto>? Containers { get; set; }
        public List<FetchGoodDto>? Goods { get; set; }
    }
}