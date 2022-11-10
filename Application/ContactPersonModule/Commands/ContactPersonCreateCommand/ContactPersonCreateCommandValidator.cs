using FluentValidation;

namespace Application.ContactPersonModule.Commands.ContactPersonCreateCommand {

    public class ContactPersonCreateCommandValidator : AbstractValidator<ContactPersonCreateCommand> {
        
        public ContactPersonCreateCommandValidator(){
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.Phone)
                .NotNull()
                .NotEmpty();
        }
    }

}