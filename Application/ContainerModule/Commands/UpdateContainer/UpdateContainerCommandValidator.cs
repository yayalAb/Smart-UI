
using FluentValidation;

namespace Application.ContainerModule.Commands.UpdateContainer
{
    public class UpdateContainerCommandValidator : AbstractValidator<UpdateContainerCommand>    
    {
        public UpdateContainerCommandValidator()
        {
            RuleFor(c => c.ContianerNumber)
              .NotNull()
              .NotEmpty();
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
        }
    }
}
