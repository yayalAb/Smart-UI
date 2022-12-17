
using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;
using static Domain.Common.DocumentType.DocumentType;

namespace Application.DocumentationModule.Commands.UpdateDocumentation
{
    public class UpdateDocumentationCommandValidator : AbstractValidator<UpdateDocumentationCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateDocumentationCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(d => d.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(d => d.OperationId)
                .NotNull()
                .Must(BeFoundInDb);
            RuleFor(d => d.Date)
                .NotNull()
                .NotEmpty()
                .WithMessage("date is not in the correct format!");
            RuleFor(d => d.Type)
                .NotNull()
                .NotEmpty()
                .MaximumLength(45)
                .Must(BeOfType)
                .WithMessage("type is not in the correct format!");
            RuleFor(d => d.BankPermit)
                .MaximumLength(45)
                .WithMessage("bank permit is not in the correct format!");
            RuleFor(d => d.InvoiceNumber)
                .MaximumLength(45)
                .WithMessage("Invoice Number is not in the correct format!");
            RuleFor(d => d.Source)
                          .MaximumLength(45)
                          .WithMessage("source is not in the correct format!");
            RuleFor(d => d.Destination)
                .MaximumLength(45)
                .WithMessage("destination is not in the correct format!");
        }
        private bool BeFoundInDb(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }

        private bool BeOfType(string Type)
        {
            return DocumentationType.Types.Contains(Type) && Type != Enum.GetName(typeof(Documents), Documents.Waybill);
        }

    }
}
