
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.OperationDocuments.Queries.Number9;

public class CreatePaymentCommandValidator : AbstractValidator<Number9>
    {
        private readonly IAppDbContext _context;

        public CreatePaymentCommandValidator(IAppDbContext context) {

            _context = context;

            RuleFor(s => s.Type)
                 .NotNull()
                 .NotEmpty()
                 .Must(BeOfType)
                 .WithMessage("invalid document type!");
            RuleFor(s => s.OperationId)
                 .NotNull()
                 .NotEmpty()
                 .Must(BeRegisteredOperationId)
                 .WithMessage("Operation with the provided id is not found");

        }
        private bool BeRegisteredOperationId(int operationId) {
            return _context.Operations.Find(operationId) != null;
        }

        private bool BeOfType(string Type) {
            return Type.ToLower() == "import" || Type.ToLower() == "transfer";
        }

    }