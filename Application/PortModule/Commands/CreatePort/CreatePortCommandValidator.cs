
using FluentValidation;

namespace Application.PortModule.Commands.CreatePort
{
    public class CreatePortCommandValidator : AbstractValidator<CreatePortCommand>  
    {
        public CreatePortCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
