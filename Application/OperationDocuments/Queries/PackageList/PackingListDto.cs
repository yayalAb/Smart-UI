using Application.OperationDocuments.Queries.Common;

namespace Application.OperationDocuments.Queries.PackageList;
public class PackingListDto : DocsDto
{
    public string? Fright { get; set; }
    public string? PlaceOfDelivery { get; set;}
}