using Application.User.Commands.AuthenticateUser;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class LoginResponse
    {
        public string tokenString { get; set; }
        public string fullName { get; set; }
        public string id { get; set; }
        public IEnumerable<UserRoleDto> roles { get; set; }  
        public string groupId { get; set; }
    }
}
