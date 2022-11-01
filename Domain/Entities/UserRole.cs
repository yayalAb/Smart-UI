using Domain.Common;

namespace Domain.Entities
{
    public class UserRole : BaseAuditableEntity
    {
        public string page { get; set; }    
        public string title { get; set; }
        public bool canAdd { get; set; } = true;
        public bool canDelete { get; set; } = true;
        public bool canViewDetail { get; set; } = true;
        public bool canView { get; set; } = true;
        public bool canUpdate { get; set; } = true; 
        public string ApplicationUserId { get; set; }
       
    }
}
