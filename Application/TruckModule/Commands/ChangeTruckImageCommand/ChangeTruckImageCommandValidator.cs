using FluentValidation;

namespace Application.TruckModule.Commands.ChangeTruckImageCommand
{

    public class ChangeTruckImageValidator : AbstractValidator<ChangeTruckImageCommand>
    {

        public ChangeTruckImageValidator()
        {

            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("truck id is not in the correct format");
            RuleFor(u => u.ImageFile)
                .NotNull()
                .NotEmpty()
                .WithMessage("truck Image is not set");

        }
    }

}