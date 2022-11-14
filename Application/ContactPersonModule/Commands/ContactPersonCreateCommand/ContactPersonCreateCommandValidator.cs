using FluentValidation;

namespace Application.ContactPersonModule.Commands.ContactPersonCreateCommand {

    public class ContactPersonCreateCommandValidator : AbstractValidator<ContactPersonCreateCommand> {
        
        public ContactPersonCreateCommandValidator(){
            RuleFor(u => u.Name)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("name is not in the correct format!");
            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .EmailAddress()
                .WithMessage("email is not in the correct format!");
            RuleFor(u => u.Phone)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("phone is not in the correct format!");
        }
    }

}