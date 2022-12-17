
using Domain.Entities;

namespace Application.OperationDocuments.Queries.CertificateOfOrigin;

public class CertificateDto
{
    public Operation operation { get; set; }
    public ICollection<Good> goods { get; set; }
}