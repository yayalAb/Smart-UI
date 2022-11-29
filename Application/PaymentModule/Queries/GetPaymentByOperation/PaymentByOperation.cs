
using Application.Common.Interfaces;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PaymentModule.Queries.GetPaymentByOperation;

public record PaymentByOperation : IRequest<OperationPaymentDto> {
    public int OperationId {get; init;}
}

public class PaymentByOperationHandler : IRequestHandler<PaymentByOperation, OperationPaymentDto>
{

    private readonly IAppDbContext _context;

    public PaymentByOperationHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<OperationPaymentDto> Handle(PaymentByOperation request, CancellationToken cancellationToken)
    {
        List<Payment> payments = await _context.Payments.Where(p => p.OperationId == request.OperationId).Include(o => o.ShippingAgent).ToListAsync();

        return new OperationPaymentDto {
            ShippingAgnetFee = from payment in payments where ShippingAgentPaymentType.Types.Contains(payment.Name) select payment,
            TerminalPortFee = from payment in payments where TerminalPortPaymentType.Types.Contains(payment.Name) select payment
        };
    }
}