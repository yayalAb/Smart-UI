using FluentValidation;

namespace Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
