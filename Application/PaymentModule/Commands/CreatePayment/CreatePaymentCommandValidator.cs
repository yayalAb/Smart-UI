

using Application.Common.Interfaces;
using Domain.Common.PaymentTypes;
using FluentValidation;

namespace Application.PaymentModule.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        private readonly IAppDbContext _context;

        public CreatePaymentCommandValidator(IAppDbContext context) {

            _context = context;

            RuleFor(s => s.Type)
                 .NotNull()
                 .NotEmpty()
                 .Must(OfType);
            RuleFor(s => s.Name)
                 .NotNull()
                 .NotEmpty();
            RuleFor(s => s.PaymentMethod)
                 .NotNull()
                 .NotEmpty();
            RuleFor(s => s.PaymentDate)
                 .NotNull()
                 .NotEmpty();
            RuleFor(s => s.Currency)
                 .NotNull()
                 .NotEmpty();
            RuleFor(s => s.Amount)
                 .NotNull()
                 .NotEmpty();
            RuleFor(s => s.OperationId)
                 .NotNull()
                 .NotEmpty()
                 .Must(BeRegisteredOperationId).WithMessage("Operation with the provided id is not found");
            RuleFor(s => s.ShippingAgentId)
                 .Must(BeRegisteredShippingAgentId).WithMessage("shipping agent with the provided id is not found");

        }
        private bool BeRegisteredOperationId(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }
        
        private bool BeRegisteredShippingAgentId(int? shippingAgentId)
        {
            return shippingAgentId == null || _context.ShippingAgents.Find(shippingAgentId) != null;
        }

        private bool OfType(string Type) {
            return ShippingAgentPaymentType.Types.Contains(Type) || TerminalPortPaymentType.Types.Contains(Type);
        }

    }
}
