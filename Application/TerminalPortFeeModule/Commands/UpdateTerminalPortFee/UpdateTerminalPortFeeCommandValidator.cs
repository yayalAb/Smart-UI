
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.TerminalPortFeeModule.Commands.UpdateTerminalPortFee
{
    public class UpdateTerminalPortFeeCommandValidator : AbstractValidator<UpdateTerminalPortFeeCommand>    
    {
        private readonly IAppDbContext _context;

        public UpdateTerminalPortFeeCommandValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(s => s.Type)
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
                 .Must(BeFoundInDb).WithMessage("operation with provided id is not found");

        }
        private bool BeFoundInDb(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }



    }
}
