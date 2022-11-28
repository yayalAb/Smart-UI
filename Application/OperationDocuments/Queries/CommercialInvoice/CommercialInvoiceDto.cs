
using Domain.Entities;

namespace Application.OperationDocuments.Queries.CommercialInvoice;

public class CommercialInvoiceDto {
    public Documentation Document {get; set;}
    public Operation Operation {get; set;}
    public ICollection<Good> Goods {get; set;}
    public ICollection<Container> Containers {get; set;}

}