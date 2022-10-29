using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(u=>u.Email)
                .NotEmpty()
                .NotNull()  
                .EmailAddress();
            RuleFor(u=>u.Password)
                .NotEmpty()
                .NotNull();    
        }
    }
}
