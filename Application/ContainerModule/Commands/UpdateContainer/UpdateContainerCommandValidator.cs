
using FluentValidation;

namespace Application.ContainerModule.Commands.UpdateContainer
{
    public class UpdateContainerCommandValidator : AbstractValidator<UpdateContainerCommand>    
    {
        public UpdateContainerCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.ContianerNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("container number is not in the correct format!");
            RuleFor(c => c.Owner)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("owner is not in the correct format!");
            RuleFor(c => c.ManufacturedDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("name is not in the correct format!");
        }
    }
}
