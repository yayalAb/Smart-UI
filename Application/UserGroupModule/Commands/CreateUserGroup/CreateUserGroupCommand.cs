

using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UserGroupModule.Commands.CreateUserGroup
{
    public record CreateUserGroupCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Responsiblity { get; set; }

        public List<UserRoleDto> UserRoles { get; set; }
    }
    public class CreateUserGroupCommandHandler : IRequestHandler<CreateUserGroupCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserGroupCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
        {
            List<AppUserRole> userRoles = _mapper.Map<List<UserRoleDto> , List<AppUserRole>>(request.UserRoles);
            using var transaction = _context.database.BeginTransaction();
            try
            {
                var newGroup = new UserGroup
                {
                    Name = request.Name,
                    Responsiblity = request.Responsiblity,
                };

                await _context.UserGroups.AddAsync(newGroup);
                await _context.SaveChangesAsync(cancellationToken);

                if (userRoles.ToArray().Length != Enum.GetNames(typeof(Page)).Length)
                {
                    userRoles = AppUserRole.fillUndefinedRoles(userRoles);
                }
                //add group id to every role
                for (int i = 0; i < userRoles.Count; i++)
                {
                    userRoles[i].UserGroupId = newGroup.Id;
                }
                await _context.AddRangeAsync(userRoles);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
                return newGroup.Id;

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                throw;
            }

        }
    }
}
