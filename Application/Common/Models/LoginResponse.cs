using Application.User.Commands.AuthenticateUser;


namespace Application.Common.Models
{
    public class LoginResponse
    {
        public string tokenString { get; set; }
        public string fullName { get; set; }
        public string id { get; set; }
        public IEnumerable<UserRoleDto> roles { get; set; }  
        public string groupId { get; set; }
        public string userName { get; set; }
    }
}
