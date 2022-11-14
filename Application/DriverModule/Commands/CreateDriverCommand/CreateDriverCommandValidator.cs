using FluentValidation;

namespace Application.DriverModule.Commands.CreateDriverCommand {

    public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand> {
        
        public CreateDriverCommandValidator(){
            
            RuleFor(u => u.Fullname)
                .NotNull()
                .NotEmpty()
                .WithMessage("fullname is not in the correct format");
            RuleFor(u => u.LicenceNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("licence number is not in the correct format");
            RuleFor(u => u.TruckId)
                .NotNull()
                .NotEmpty()
                .WithMessage("fullname is not in the correct format");
            RuleFor(d => d.ImageFile)
                .NotNull()
                .NotEmpty()
                .WithMessage("image is not in the correct format");
        }
    }

}