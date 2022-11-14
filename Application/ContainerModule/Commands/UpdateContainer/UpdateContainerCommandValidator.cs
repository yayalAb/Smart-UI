
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.ContainerModule.Commands.UpdateContainer
{
    public class UpdateContainerCommandValidator : AbstractValidator<UpdateContainerCommand>    
    {
        private readonly IAppDbContext _context;
        public UpdateContainerCommandValidator(IAppDbContext context)
        {
<<<<<<< HEAD
            RuleFor(c => c.Id)
=======
            _context = context;
            RuleFor(c => c.ContianerNumber)
>>>>>>> 68a589be846fc74a04d36d873b4bfefc93d8539c
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
<<<<<<< HEAD
            RuleFor(c => c.ContianerNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("container number is not in the correct format!");
            RuleFor(c => c.Owner)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("owner is not in the correct format!");
            RuleFor(c => c.ManufacturedDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("name is not in the correct format!");
=======
            RuleFor(c => c.OperationId)
                .NotNull()
                .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");

        }

        private bool BeFoundInDb(int operationId)
        {
            return _context.UserGroups.Find(operationId) != null;
>>>>>>> 68a589be846fc74a04d36d873b4bfefc93d8539c
        }
    }
}