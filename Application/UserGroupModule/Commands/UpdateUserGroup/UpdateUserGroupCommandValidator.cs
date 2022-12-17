
using FluentValidation;

namespace Application.UserGroupModule.Commands.UpdateUserGroup
{
    public class UpdateUserGroupCommandValidator : AbstractValidator<UpdateUserGroupCommand>
    {
        public UpdateUserGroupCommandValidator()
        {
            RuleFor(g => g.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
