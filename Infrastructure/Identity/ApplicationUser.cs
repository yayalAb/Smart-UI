


using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string FullName { get; set; }
        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }
        public IEnumerable<AppUserRole> UserRoles { get ; set; }
       
    }
}
