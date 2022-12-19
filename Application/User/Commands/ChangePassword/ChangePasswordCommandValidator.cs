
using FluentValidation;

namespace Application.User.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
            RuleFor(u => u.NewPassword)
                .NotNull()
                .NotEmpty()
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$").WithMessage("password must be atleast 6 digit long and must contain atlist one number one number and 1 special character")
                ;
        }
    }
}
