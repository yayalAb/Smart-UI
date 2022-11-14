

using Domain.Enums;
using FluentValidation;

namespace Application.UserGroupModule.Commands.CreateUserGroup
{
    public class CreateUserGroupCommandValidator : AbstractValidator<CreateUserGroupCommand>
    {
        public CreateUserGroupCommandValidator()
        {
            RuleFor(g => g.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(g => g.UserRoles)
                .Must(AllHaveValidPage).WithMessage(" one or more userRole  have invalid page name ");


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

