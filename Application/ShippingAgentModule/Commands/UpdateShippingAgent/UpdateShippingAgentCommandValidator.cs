using System.Text.RegularExpressions;
using FluentValidation;


namespace Application.ShippingAgentModule.Commands.UpdateShippingAgent
{
    public class UpdateShippingAgentCommandValidator : AbstractValidator<UpdateShippingAgentCommand>
    {
        public UpdateShippingAgentCommandValidator()
        {

            RuleFor(sha => sha.FullName)
               .NotEmpty()
               .NotNull()
               .MaximumLength(45);
            RuleFor(sha => sha.Address)
                .NotNull();
            RuleFor(sha => sha.Address.Email)
                .EmailAddress();
            RuleFor(sha => sha.Image)
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
