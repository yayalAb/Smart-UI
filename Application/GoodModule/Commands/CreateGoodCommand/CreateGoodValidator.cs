using FluentValidation;

namespace Application.GoodModule.Commands.CreateGoodCommand {
    public class CreateGoodValidator : AbstractValidator<CreateGoodCommand> {

        public CreateGoodValidator(){

            RuleFor(u => u.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("description is not in the correct format");
            RuleFor(u => u.HSCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("HSCode is not in the correct format");
            RuleFor(u => u.Manufacturer)
                .NotNull()
                .NotEmpty()
                .WithMessage("Manufacturer is not in the correct format");
            RuleFor(u => u.Weight)
                .NotNull()
                .NotEmpty()
                .WithMessage("Weight is not in the correct format");
            RuleFor(u => u.Quantity)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("quantity is not in the correct format");
            // RuleFor(u => u.ContainerId)
            //     .NotNull()
            //     .NotEmpty()
            //     .NotEqual(0)
            //     .WithMessage("container id not set");
        }

    }
};