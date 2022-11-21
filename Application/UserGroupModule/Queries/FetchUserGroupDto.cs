

using Application.Common.Mappings;
using Domain.Entities;

namespace Application.UserGroupModule.Commands
{
    public class FetchUserRoleDto : IMapFrom<AppUserRole>
    {
        public int Id { get; set; }
        public string Page { get; set; }
        public string Title { get; set; }
        public bool CanAdd { get; set; } = true;
        public bool CanDelete { get; set; } = true;
        public bool CanViewDetail { get; set; } = true;
        public bool CanView { get; set; } = true;
        public bool CanUpdate { get; set; } = true;
        public int UserGroupId { get; set; }
    }
}
