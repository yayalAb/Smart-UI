
using Domain.Common;

namespace Domain.Entities
{
    public class UserGroup : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Responsiblity { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; } = null!;

    }
}