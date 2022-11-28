using Application.Common.Mappings;
using Domain.Entities;

namespace Application.GoodModule.Queries;
public class OperationDto2 : IMapFrom<Operation>{
    public int Id {get; set;}
    public string OperationNumber { get; set; } = null!;
}