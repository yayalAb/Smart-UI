using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }    
        public string Email { get; set; }   
        public string PasswordHash { get; set; }    
        public string FullName { get; set; }
        public string GroupId { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
