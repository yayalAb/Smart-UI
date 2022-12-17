

using Application.Common.Interfaces;
using FluentValidation;

namespace Application.ContainerModule.Commands.CreateContainer
{
    public class CreateContainerCommandValidator : AbstractValidator<CreateContainerCommand>
    {
        private readonly IAppDbContext _context;

        public CreateContainerCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(c => c.ContainerNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .WithMessage("container number is not in the correct format!");
            RuleFor(c => c.SealNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Size)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.OperationId)
                .NotNull()
                .Must(BeFoundInDb).WithMessage("operation with the provided id is not found");
            RuleFor(c => c.LocationPortId)
                .NotNull()
                .Must(BeFoundInPort).WithMessage("port not found with the provided id");

        }

        private bool BeFoundInDb(int operationId)
        {
            return _context.UserGroups.Find(operationId) != null;
        }

        private bool BeFoundInPort(int PortId)
        {
            return _context.Ports.Find(PortId) != null;
        }
    }
}
