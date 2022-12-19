using FluentValidation;

namespace Application.AddressModule.Commands.AddressCreateCommand
{

    public class AddressCreateCommandValidator : AbstractValidator<AddressCreateCommand>
    {

        public AddressCreateCommandValidator()
        {

            RuleFor(u => u.City)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty();
            RuleFor(u => u.Subcity)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty();
            RuleFor(u => u.Country)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty();
            RuleFor(u => u.Region)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty();
            RuleFor(u => u.Email)
                .NotEmpty()
                .MaximumLength(45)
                .NotNull()
                .EmailAddress().WithMessage("email is not in the correct format");
            RuleFor(u => u.Phone)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty();

        }

    }

}