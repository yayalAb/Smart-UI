using System.Data;

using FluentValidation;

namespace Application.PortModule.Commands.CreatePort
{
    public class CreatePortCommandValidator : AbstractValidator<CreatePortCommand>  
    {
        public CreatePortCommandValidator()
        {
            RuleFor(c => c.PortNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("port number is not in the correct format");
            RuleFor(c => c.Country)
                .MaximumLength(45)
                .WithMessage("country name is not in the correct format");
            RuleFor(c => c.Region)
                .MaximumLength(45)
                .WithMessage("region name is not in the correct format");
            RuleFor(c => c.Vollume)
                .MaximumLength(45)
                .WithMessage("vollume is not in the correct format");
        }
    }
}
