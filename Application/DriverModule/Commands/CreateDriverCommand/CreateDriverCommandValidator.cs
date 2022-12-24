using System.Text.RegularExpressions;
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
            RuleFor(u => u.Image)
                .Must(BeValidBase64String).WithMessage("image is not in the correct base64string format");
            RuleFor(u => u.address.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("invalid email address");
        }

        private bool BeValidBase64String(string? base64String)
        {
            if (base64String == null)
            {
                return true;
            }
            try
            {
                Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
                base64String = regex.Replace(base64String, string.Empty);

                Convert.FromBase64String(base64String);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }

}