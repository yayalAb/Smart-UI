using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;

namespace Application.GeneratedDocumentModule.Queries;
public class GetAllGeneratedDocumentsQueryValidator : AbstractValidator<GetAllGeneratedDocumentsQuery>
{
    private readonly IAppDbContext _context;

    public GetAllGeneratedDocumentsQueryValidator(IAppDbContext context)
    {
        _context = context;

        RuleFor(q => q.OperationId)
            .NotNull()
            .Must(BeFoundInOperationsTable).WithMessage("operation with the provided id is not found!");
        RuleFor(q => q.documentType)
            .NotNull()
            .Must(BeOfTypeDocuments).WithMessage("document type is not valid");
    }

    private bool BeOfTypeDocuments(string documentType)
    {
        List<string> types = new List<string>{
            Enum.GetName(typeof(Documents),Documents.TransferNumber9)!.ToUpper(),
            Enum.GetName(typeof(Documents),Documents.Number1)!.ToUpper(),
            Enum.GetName(typeof(Documents),Documents.Number4)!.ToUpper()
        };
        return types.Contains(documentType.ToUpper());
    }

    private bool BeFoundInOperationsTable(int operationId)
    {
        return _context.Operations.Find(operationId) != null;
    }
}