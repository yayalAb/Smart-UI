

using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;

namespace Application.UserGroupModule.Commands.CreateUserGroup
{
    public class CreateUserGroupCommandValidator : AbstractValidator<CreateUserGroupCommand>
    {
        private readonly IAppDbContext _context;

        public CreateUserGroupCommandValidator(IAppDbContext context)
        {
            RuleFor(g => g.Name)
                .NotNull()
                .NotEmpty()
                .Must(BeUnique).WithMessage("group name should be unique");
            RuleFor(g => g.UserRoles)
                .Must(AllHaveValidPage).WithMessage(" one or more userRole  have invalid page name ");
            _context = context;
        }


        private bool BeUnique(CreateUserGroupCommand userGroup, string name)
        {
            return !_context.UserGroups.Where(ug => ug.Name == name).Any();
        }
        private bool AllHaveValidPage(List<UserRoleDto> userRoles)
        {
            if (userRoles == null || userRoles.Count == 0)
            {
                return true;
            }
            foreach (UserRoleDto userRole in userRoles)
            {
                if (!Enum.GetNames(typeof(Page)).Any(p => p.ToLower() == userRole.Page.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

