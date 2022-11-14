﻿
using Application.Common.Interfaces;
using FluentValidation;

namespace Application.ShippingAgentFeeModule.Commands.UpdateShippingAgentFee
{
    public class UpdateShippingAgentFeeCommandValidator : AbstractValidator<UpdateShippingAgentFeeCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateShippingAgentFeeCommandValidator(IAppDbContext context)
        {
            _context = context;
            RuleFor(s => s.Id)
                .NotNull();
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
                    .Must(BeRegisteredOperationId).WithMessage("Operation with the provided id is not found");
            RuleFor(s => s.ShippingAgentId)
                 .NotNull()
                 .NotEmpty()
                 .Must(BeRegisteredShippingAgentId).WithMessage("shipping agent with the provided id is not found");

        }
        private bool BeRegisteredOperationId(int operationId)
        {
            return _context.Operations.Find(operationId) != null;
        }
        private bool BeRegisteredShippingAgentId(int shippingAgentId)
        {
            return _context.ShippingAgents.Find(shippingAgentId) != null;
        }


    }
}