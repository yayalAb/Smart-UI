using Application.Common.Interfaces;
using Application.User.Commands.AuthenticateUser;
using Domain.Enums;
using FluentValidation;

namespace Application.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IAppDbContext _context;

        public CreateUserCommandValidator(IAppDbContext context)
        {

            _context = context;

            RuleFor(u => u.FullName)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(u => u.Address.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$")
                .WithMessage("password must be atleast 6 digit long and must contain atlist one number one number and 1 special character");
            RuleFor(u => u.GroupId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInDb).WithMessage("group with the provided id is not found");


        }

        private bool BeFoundInDb(int groupId)
        {
            return _context.UserGroups.Find(groupId) != null;
        }

    }
}