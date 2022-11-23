using FluentValidation;

namespace Application.GoodModule.Commands.UpdateGoodCommand {
    public class UpdateGoodValidator : AbstractValidator<UpdateGoodCommand> {

        public UpdateGoodValidator(){

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
        }

    }
}