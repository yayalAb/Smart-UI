using Application.Common.Interfaces;
using FluentValidation;

namespace Application.TruckAssignmentModule.Commands.CreateTruckAssignment
{

    public class CreateTruckAssignmentCommandValidator : AbstractValidator<CreateTruckAssignmentCommand>
    {
        private readonly IAppDbContext _context;

        public CreateTruckAssignmentCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(u => u.OperationId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInOperationsTable).WithMessage("operation with the provided id is not found");
            RuleFor(u => u.DriverId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInDriversTable).WithMessage("driver with the provided id is not found");
                ;
            RuleFor(u => u.TruckId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInDriversTable).WithMessage("driver with the provided id is not found");
                
            RuleFor(u => u.SourcePortId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInPortsTable).WithMessage("source port with the provided id is not found");

            RuleFor(u => u.DestinationPortId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInPortsTable).WithMessage("destination port with the provided id is not found");
            When(ta => ta.ContainerIds == null, () => {
                RuleFor(ta => ta.GoodIds)
                    .NotNull()
                    .NotEmpty().WithMessage("both goodIds and containerIds cannot be null or empty at the same time");
     
        });

        }
        private bool BeFoundInOperationsTable(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }
        private bool BeFoundInDriversTable(int driverId)
        {
            return _context.Drivers.Find(driverId) != null;
        }
        private bool BeFoundInTrucksTable(int truckId)
        {
            return _context.Trucks.Find(truckId) != null;
        }
        private bool BeFoundInPortsTable(int portId)
        {
            return _context.Ports.Find(portId) != null;
        }
    }

}