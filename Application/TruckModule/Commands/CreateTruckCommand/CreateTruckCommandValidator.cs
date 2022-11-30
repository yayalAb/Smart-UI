using FluentValidation;

namespace Application.TruckModule.Commands.CreateTruckCommand {

    public class CreateTruckCommandValidator : AbstractValidator<CreateTruckCommand> {
        
        public CreateTruckCommandValidator(){
            RuleFor(u => u.TruckNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("truck number is not in the correct format");
            RuleFor(u => u.Capacity)
                .NotNull()
                .NotEmpty()
                .WithMessage("capacity is not in the correct format");
            RuleFor(u => u.Type)
                .NotNull()
                .NotEmpty()
                .WithMessage("type is not in the correct format");
            RuleFor(u => u.PlateNumber)
                .NotNull()
                .NotEmpty();
        }
    }

}