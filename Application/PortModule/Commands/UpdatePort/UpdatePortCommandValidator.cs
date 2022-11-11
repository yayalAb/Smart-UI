

using FluentValidation;

namespace Application.PortModule.Commands.UpdatePort
{
    public class UpdatePortCommandValidator : AbstractValidator<UpdatePortCommand>  
    {
        public UpdatePortCommandValidator()
        {

            RuleFor(c => c.PortNumber)
                .NotNull()
                .NotEmpty();
        }
    }
}
