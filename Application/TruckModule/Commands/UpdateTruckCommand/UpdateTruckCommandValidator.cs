using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.TruckModule.Commands.UpdateTruckCommand
{

    public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruckCommand>
    {

        public UpdateTruckCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("truck id is not in the correct format");
            RuleFor(u => u.TruckNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("truck number is not in the correct format");
            RuleFor(u => u.Capacity)
                .NotNull()
                .NotEmpty()
                .WithMessage("capacity is not in the correct format");
            RuleFor(u => u.Type)
                .NotNull()
                .NotEmpty()
                .WithMessage("type is not in the correct format");
            RuleFor(u => u.PlateNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(u => u.Image)
              .Must(BeValidBase64String).WithMessage("image is not in the correct base64string format");
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