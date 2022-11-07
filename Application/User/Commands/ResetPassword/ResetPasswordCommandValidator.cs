
using FluentValidation;

namespace Application.User.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>    
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(u => u.Password)
              .NotNull()
              .NotEmpty()
              .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$").WithMessage("password must be atleast 6 digit long and must contain atlist one number one number and 1 special character")
              ;
            RuleFor(u => u.Token)
                .NotEmpty()
                .NotNull();
        }
    }
}
