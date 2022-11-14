
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.ContainerModule.Commands.UpdateContainer
{
    public class UpdateContainerCommandValidator : AbstractValidator<UpdateContainerCommand>    
    {
        private readonly IAppDbContext _context;
        public UpdateContainerCommandValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(c => c.ContianerNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.OperationId)
                .NotNull()
                .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");

        }

        private bool BeFoundInDb(int operationId)
        {
            return _context.UserGroups.Find(operationId) != null;
        }
    }
}