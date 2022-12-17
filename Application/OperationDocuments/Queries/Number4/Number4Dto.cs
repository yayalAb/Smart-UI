
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Number4;

public class Number4Dto
{
    public Company company { get; set; }
    public Operation operation { get; set; }
    public ICollection<Container> containers { get; set; }
    public Payment doPayment { get; set; }
    public ICollection<Good> goods { get; set; }
}