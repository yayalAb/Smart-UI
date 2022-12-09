
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Gatepass.GPDtos;

public class GPTruckAssignmentDto : IMapFrom<TruckAssignment> {
    public GPOperationDto Operation {get; set;}
    public ICollection<GPGoodDto>? Good {get; set;}
    public string TruckNumber {get; set;}
}