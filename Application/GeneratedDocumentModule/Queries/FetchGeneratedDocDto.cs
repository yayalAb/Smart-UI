using Application.Common.Mappings;
using Application.Common.Service;
using Application.CompanyModule.Queries;
using Application.GoodModule.Queries;
using Application.PortModule;
using Domain.Entities;

namespace Application.GeneratedDocumentModule.Queries;
public class FetchGeneratedDocumentDto : IMapFrom<GeneratedDocument>
{
    public int Id { get; set; }
    public string LoadType { get; set; } // container , unstaff
    public string DocumentType { get; set; } /// t1 , no.1....
    public int OperationId { get; set; }
    public DateTime GeneratedDate {get; set; }
    // public OperationDto2 Operation { get; set; }
    public ContactPersonDto ContactPerson { get; set; }
    public PortDto DestinationPort { get; set; }
    // public ICollection<ContainerDto2> Containers { get; set; }
    // public ICollection<GeneratedDocumentsGoodsDto> GeneratedDocumentsGoods {get; set; }
}