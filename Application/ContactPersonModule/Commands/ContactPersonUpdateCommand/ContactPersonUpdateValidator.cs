using FluentValidation;

namespace Application.ContactPersonModule.Commands.ContactPersonUpdateCommand
{

    public class ContactPersonUpdateValidator : AbstractValidator<ContactPersonUpdateCommand>
    {
        public ContactPersonUpdateValidator()
        {
            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("name is not in the correct format!");
            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(45)
                .WithMessage("email is not in the correct format!");
            RuleFor(u => u.Phone)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("phone is not in the correct format!");
        }
    }

}