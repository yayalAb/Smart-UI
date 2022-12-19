
using Domain.Entities;

namespace Application.OperationDocuments.Queries.PackageList;

public class PackageListDto
{
    public Operation operation { get; set; }
    public Documentation documentation { get; set; }
    public ICollection<Good> goods { get; set; }
    public ICollection<Container> containers { get; set; }

}