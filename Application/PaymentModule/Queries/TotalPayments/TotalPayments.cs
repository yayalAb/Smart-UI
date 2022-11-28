
using Application.Common.Interfaces;
using MediatR;

namespace Application.PaymentModule.Queries.TotalPayments;

public record TotalPayments : IRequest<TotalPaymentDto> {}

public class TotalPaymentsHandler : IRequestHandler<TotalPayments, TotalPaymentDto>
{

    private readonly IAppDbContext _context;

    public TotalPaymentsHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<TotalPaymentDto> Handle(TotalPayments request, CancellationToken cancellationToken)
    {
        
        var totalShppingAgentPayment = _context.Payments.Where(p => p.ShippingAgentId != null && p.ShippingAgentId != 0).Sum(p => p.Amount);
        var totalTerminalPortFee = _context.Payments.Where(p => p.ShippingAgentId == null || p.ShippingAgentId == 0).Sum(p => p.Amount);
        
        return new TotalPaymentDto {
            TerminalPortFee = (int) totalTerminalPortFee,
            ShppingAgentFee = (int) totalShppingAgentPayment
        };

    }
}