

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
                .NotEmpty();
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();

        }
    }
}
