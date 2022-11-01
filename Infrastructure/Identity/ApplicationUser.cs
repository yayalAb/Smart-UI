


using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string FullName { get; set; }
        public string GroupId { get; set; }
        public IEnumerable<UserRole> UserRoles { get ; set; }
    }
}
