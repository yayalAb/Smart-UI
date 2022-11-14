

using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.ContainerModule.Commands.CreateContainer
{
    public class CreateContainerCommandValidator : AbstractValidator<CreateContainerCommand>  
    {
        public CreateContainerCommandValidator()
        {
            RuleFor(c => c.ContianerNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("container number is not in the correct format!");
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
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
