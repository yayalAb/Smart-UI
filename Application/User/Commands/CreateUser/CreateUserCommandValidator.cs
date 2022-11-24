using Application.Common.Interfaces;
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
                .EmailAddress()
                // .Matches("/^(([^<>()[\\]\\.,;:\\s@\"]+(\\.[^<>()[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$/")
                .WithMessage("invalid email address");
            RuleFor(u => u.UserGroupId)
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