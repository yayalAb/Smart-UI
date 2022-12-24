using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;

namespace Application.TruckAssignmentModule.Commands.CreateTruckAssignment
{

    public class CreateTruckAssignmentCommandValidator : AbstractValidator<CreateTruckAssignmentCommand>
    {
        private readonly IAppDbContext _context;

        public CreateTruckAssignmentCommandValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(u => u.GatePassType)
                .NotNull()
                .Must(BeValidGatepassType)
                .WithMessage("gatepass type is incorrect");
            RuleFor(u => u.AgreedTariff)
                .NotNull();
            RuleFor(u => u.Currency)
                .NotNull();
            When(u => u.GatePassType.ToUpper() == Enum.GetName(typeof(GatepassType), GatepassType.EXIT), () =>
            {
                RuleFor(u => u.SENumber)
                    .NotNull()
                    .WithMessage("SENumber cannot be null if gatepass is EXIT type !!");

            });
            RuleFor(u => u.OperationId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInOperationsTable).WithMessage("operation with the provided id is not found");
            RuleFor(u => u.DriverId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInDriversTable).WithMessage("driver with the provided id is not found")
                .Must(BeUnassignedDriver).WithMessage("driver is already assigned for another truck assignment ; release the driver before assigning again");
            
            RuleFor(u => u.TruckId)
                .NotNull()
                .NotEmpty()
                .Must(BeFoundInTrucksTable).WithMessage("truck with the provided id is not found")
                .Must(BeUnassignedTruck).WithMessage("truck is already assigned for another truck assignment ; release the truck before assigning again");
            
            RuleFor(u => u.SourceLocation)
                .NotNull()
                .NotEmpty();

            RuleFor(u => u.DestinationLocation)
               .NotNull()
               .NotEmpty();

            RuleFor(u => u.SourcePortId)
                .Must(BeFoundInPortsTable).WithMessage("source port with the provided id is not found");

            RuleFor(u => u.DestinationPortId)
                .Must(BeFoundInPortsTable).WithMessage("destination port with the provided id is not found");
            When(ta => ta.ContainerIds == null, () =>
            {
                RuleFor(ta => ta.GoodIds)
                    .NotNull()
                    .NotEmpty().WithMessage("both goodIds and containerIds cannot be null or empty at the same time");

            });

        }

        private bool BeUnassignedDriver(int driverId)
        {
            var driver = _context.Drivers.Find(driverId);
            return !driver!.IsAssigned;
        }
        private bool BeUnassignedTruck(int truckId)
        {
            var truck = _context.Trucks.Find(truckId);
            return !truck!.IsAssigned;
        }
        private bool BeValidGatepassType(string gatepassType)
        {
            return Enum.IsDefined(typeof(GatepassType), gatepassType.ToUpper());
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
        private bool BeFoundInPortsTable(int? portId)
        {
            return portId == null || _context.Ports.Find(portId) != null;
        }

    }

}