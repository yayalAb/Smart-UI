using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationDocuments.Queries.Number1;
public class GenerateNumber1QueryValidator : AbstractValidator<GenerateNumber1Query>{
    private readonly IAppDbContext _context;

    public GenerateNumber1QueryValidator(IAppDbContext context)
    {
        _context = context;
        RuleFor(n => n.OperationId)
            .NotNull()
            .NotEmpty()
            .Must(BeFoundInOperations).WithMessage("operation with the provided id is not found");
        RuleFor(n => n.DestinationPortId)
            .NotNull()
            .NotEmpty()
            .Must(BeFoundInPortTable).WithMessage("destination port with the provided id is not found");
        RuleFor(n => n.NameOnPermitId)
            .NotNull()
            .NotEmpty()
            .Must(BeFoundInContactPeopleTable).WithMessage("contact person with the provided id is not found");
        
    }

    private bool BeFoundInContactPeopleTable(int nameOnPermitId)
    {
        return _context.ContactPeople.Find(nameOnPermitId) != null;
    }

    private bool BeFoundInOperations(int operationId)
    {
        return _context.Operations.Find(operationId) != null;
    }
        private bool BeFoundInPortTable(int portId)
    {
        return _context.Ports.Find(portId) != null;
    }
}