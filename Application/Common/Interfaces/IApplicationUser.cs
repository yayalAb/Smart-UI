using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApplicationUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public int UserGroupId { get; set; }
        public int AddressId { get; set; }

        public UserGroup UserGroup { get; set; }
        public Address Address { get; set; }


    }
}
