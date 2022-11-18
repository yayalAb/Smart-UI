using FluentValidation;
using Application.Common.Interfaces;
using Domain.Enums;

namespace Application.User.Commands.UpdateUserCommand;

public class UpdateUserValidator : AbstractValidator<UpdateUser> {
    private readonly IAppDbContext _context;

        public UpdateUserValidator(IAppDbContext context)
        {

            _context = context;

            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty();
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