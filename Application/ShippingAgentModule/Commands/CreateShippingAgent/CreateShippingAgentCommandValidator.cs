

using FluentValidation;

namespace Application.ShippingAgentModule.Commands.CreateShippingAgent
{
    public class CreateShippingAgentCommandValidator : AbstractValidator<CreateShippingAgentCommand>
    {
        public CreateShippingAgentCommandValidator()
        {
            RuleFor(sha => sha.FullName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(45);
            RuleFor(sha => sha.Address)
                .NotNull();
            RuleFor(sha => sha.Address.Email)
                .EmailAddress();

        }
    }
}
