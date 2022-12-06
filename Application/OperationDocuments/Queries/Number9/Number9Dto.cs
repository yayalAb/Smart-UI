
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Number9;

public class Number9Dto {
    public Company company {get; set;}
    public Operation operation {get; set;}
    public Payment doPayment {get; set;}
    public ICollection<Good> goods {get; set;}
    public IEnumerable<float> containerSize {get; set;}
}