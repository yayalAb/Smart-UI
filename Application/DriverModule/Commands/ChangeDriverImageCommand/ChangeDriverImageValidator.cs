using FluentValidation;

namespace Application.DriverModule.Commands.ChangeDriverImageCommand {
    public class ChangeDriverImageValidator : AbstractValidator<ChangeDriverImage> {

        public ChangeDriverImageValidator(){
            
            RuleFor(u => u.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id is not in the correct format");
            RuleFor(u => u.ImageFile)
                .NotNull()
                .NotEmpty()
                .WithMessage("Image file is not set");
        }

    }
};