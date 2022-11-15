using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string FullName { get; set; }
        public int UserGroupId { get; set; }
        public int AddressId {get; set;}
        public byte State {get; set;}
        public UserGroup UserGroup { get; set; }
      
    }
}
