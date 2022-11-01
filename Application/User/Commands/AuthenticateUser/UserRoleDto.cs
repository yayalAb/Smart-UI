using Application.Common.Mappings;
using Domain.Entities;


namespace Application.User.Commands.AuthenticateUser
{
    public class UserRoleDto : IMapFrom<UserRole>
    {
        public string page { get; set; }
        public string title { get; set; }
        public bool canAdd { get; set; }
        public bool canDelete { get; set; } 
        public bool canView { get; set; } 
        public bool canViewDetail { get; set; } 
        public bool canUpdate { get; set; }
    }
}
