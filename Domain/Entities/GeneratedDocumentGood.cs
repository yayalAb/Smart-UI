using Domain.Common;

namespace Domain.Entities;
public class GeneratedDocumentGood : BaseAuditableEntity
{
    public int Quantity { get; set; }
    public int GoodId { get; set; } 
    public int GeneratedDocumentId { get; set; }
    public virtual GeneratedDocument GeneratedDocument { get; set; }
    public virtual Good Good { get; set; }

}