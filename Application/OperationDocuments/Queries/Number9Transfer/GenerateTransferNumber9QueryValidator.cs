using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationDocuments.Queries.Number9Transfer;
public class GenerateTransferNumber9QueryValidator: AbstractValidator<GenerateTransferNumber9Query>
{
    private readonly IAppDbContext _context;

    public GenerateTransferNumber9QueryValidator(IAppDbContext context)
    {
       _context = context;

        When(n => !n.isPrintOnly, () =>
        {
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
        });
        When(n => n.isPrintOnly, () =>
        {
            RuleFor(n => n.GeneratedDocumentId)
            .NotNull()
            .NotEmpty()
            .Must(BeFoundInGeneratedDocTable).WithMessage("generated document with the provided id is not found");
        });

    }

    private bool BeFoundInGeneratedDocTable(int? generatedDocumentId)
    {
        return _context.GeneratedDocuments.Find(generatedDocumentId) != null;
    }

    private bool BeFoundInContactPeopleTable(int? nameOnPermitId)
    {
        return nameOnPermitId == null || _context.ContactPeople.Find(nameOnPermitId) != null;
    }

    private bool BeFoundInOperations(int operationId)
    {
        return _context.Operations.Find(operationId) != null;
    }
    private bool BeFoundInPortTable(int? portId)
    {
        return portId == null || _context.Ports.Find(portId) != null;
    }
}