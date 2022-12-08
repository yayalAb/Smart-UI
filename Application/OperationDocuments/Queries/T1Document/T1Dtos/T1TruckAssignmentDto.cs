using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.T1Document.T1Dtos;

public class T1TruckAssignmentDto : IMapFrom<TruckAssignment> {
    public T1TruckDto AssignedTruck {get; set;}
    public ICollection<T1GoodDto>? AssignedGood {get; set;}
}