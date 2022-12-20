using Application.OperationDocuments.Number9Transfer;
using Domain.Enums;

namespace Application.Common.Service
{
    public  class CreateGeneratedDocDto 
    {
    public int OperationId { get; set; }
    public int NameOnPermitId { get; set; }
    public int DestinationPortId { get; set; }
    public Documents documentType { get; set; }
    public IEnumerable<int>? ContainerIds { get; set; }
    public IEnumerable<GoodWithQuantityDto>? GoodIds { get; set; }
    }
}