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
        }
    }
}
