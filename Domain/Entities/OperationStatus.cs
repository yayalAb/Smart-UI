

using Domain.Common;

namespace Domain.Entities
{
    public class OperationStatus : BaseAuditableEntity
    {
        public string GeneratedDocumentName { get; set; }   
        public DateTime GeneratedDate { get; set; }
        public bool IsApproved { get; set; } = false; 
        public DateTime? ApprovedDate { get; set; }
        public int OperationId { get; set; } 
        public virtual Operation Operation { get; set; } = null!;
    }
}
