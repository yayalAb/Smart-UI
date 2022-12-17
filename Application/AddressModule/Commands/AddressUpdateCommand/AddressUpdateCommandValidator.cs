using FluentValidation;

namespace Application.AddressModule.Commands.AddressUpdateCommand
{

    public class AddressUpdateCommandValidator : AbstractValidator<AddressUpdateCommand>
    {

        public AddressUpdateCommandValidator()
        {

            RuleFor(u => u.City)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("city is not in the correct format!");
            RuleFor(u => u.Subcity)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("subcity is not in the correct format!");
            RuleFor(u => u.Country)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("country is not in the correct format!");
            RuleFor(u => u.Region)
                .NotNull()
                .MaximumLength(45)
                .NotEmpty()
                .WithMessage("region is not in the correct format!");
            RuleFor(u => u.Email)
                .NotEmpty()
                .MaximumLength(45)
                .NotNull()
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