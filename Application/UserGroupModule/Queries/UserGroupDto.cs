

using Application.Common.Mappings;
using Application.UserGroupModule.Commands;
using Domain.Entities;

namespace Application.UserGroupModule.Queries
{
    public class UserGroupDto : IMapFrom<UserGroup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Responsiblity { get; set; }
        public ICollection<FetchUserRoleDto> UserRoles { get; set; }
    }
}
