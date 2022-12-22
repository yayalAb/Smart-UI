using Application.Common.Mappings;
using Application.OperationDocuments.Queries;
using Domain.Entities;

namespace Application.Common.Service;
public class GeneratedDocumentDto : IMapFrom<GeneratedDocument>
{
    public int Id { get; set; }
    public string LoadType { get; set; } // container , unstaff
    public string DocumentType { get; set; } /// t1 , no.1....
    public Operation Operation { get; set; }
    public Port DestinationPort { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public List<Container> Containers { get; set; }
    public List<DocGoodDto> Goods { get; set; }
}