using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Service;
public class GeneratedDocumentDto : IMapFrom<GeneratedDocument>
{
    public string LoadType { get; set; } // container , unstaff
    public string DocumentType { get; set; } /// t1 , no.1....
    public Operation Operation { get; set; }
    public Port DestinationPort { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ICollection<Container> Containers { get; set; }
    public ICollection<GeneratedDocumentsGoodsDto> GeneratedDocumentsGoods { get; set; }
}