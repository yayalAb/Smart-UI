
using Application.UserGroupModule.Commands;

namespace Application.Common.Models
{
    public class LoginResponse
    {
        public string tokenString { get; set; }
        public string fullName { get; set; }
        public string id { get; set; }
        public IEnumerable<UserRoleDto> roles { get; set; }
        public int userGroupId { get; set; }
        public string userName { get; set; }
    }
}
