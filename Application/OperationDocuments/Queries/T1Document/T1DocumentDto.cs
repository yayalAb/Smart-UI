
using Domain.Entities;

namespace Application.OperationDocuments.Queries.T1Document;

public class T1DocumentDto {
    public ICollection<TruckAssignment> TruckAssignments {get; set;}
    public Operation Operation {get; set;}
}