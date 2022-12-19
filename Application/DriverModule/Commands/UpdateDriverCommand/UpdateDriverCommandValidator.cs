using FluentValidation;

namespace Application.DriverModule.Commands.UpdateDriverCommand;

public class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
{

    public UpdateDriverCommandValidator()
    {

        RuleFor(u => u.Fullname)
            .NotNull()
            .NotEmpty()
            .WithMessage("fullname is not in the correct format");
        RuleFor(u => u.LicenceNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("licence number is not in the correct format");
        RuleFor(d => d.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("id is not set");
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