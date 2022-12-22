using Application.OperationDocuments.Queries.Number9Transfer;

namespace WebApi.Models;
public class GenerateDocRequest {
     public int OperationId { get; set; }
    public int NameOnPermitId { get; set; }
    public int DestinationPortId { get; set; }
    public IEnumerable<int>? ContainerIds { get; set; }
    public IEnumerable<GoodWithQuantityDto>? GoodIds { get; set; }
}