using FluentValidation;

namespace Application.DriverModule.Commands.CreateDriverCommand
{

    public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
    {

        public CreateDriverCommandValidator()
        {

            RuleFor(u => u.Fullname)
                .NotNull()
                .NotEmpty()
                .WithMessage("fullname is not in the correct format");
            RuleFor(u => u.LicenceNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("licence number is not in the correct format");
            RuleFor(u => u.address)
                .NotNull();
            RuleFor(u => u.address.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("invalid email address");
        }
    }

}