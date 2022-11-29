using FluentValidation;

namespace Application.TruckModule.Commands.UpdateTruckCommand {

    public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruckCommand> {
        
        public UpdateTruckCommandValidator(){
            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("truck id is not in the correct format");
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
            RuleFor(u => u.Capacity)
                .NotNull()
                .NotEmpty();
        }
    }

}