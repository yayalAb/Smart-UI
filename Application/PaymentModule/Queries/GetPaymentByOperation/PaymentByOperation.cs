
using Application.Common.Interfaces;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PaymentModule.Queries.GetPaymentByOperation;

public record PaymentByOperation : IRequest<ICollection<OperationPaymentDto>> {
    public int OperationId {get; init;}
}

public class PaymentByOperationHandler : IRequestHandler<PaymentByOperation, ICollection<OperationPaymentDto>> {

    private readonly IAppDbContext _context;

    public PaymentByOperationHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<ICollection<OperationPaymentDto>> Handle(PaymentByOperation request, CancellationToken cancellationToken)
    {
        List<Payment> payments = await _context.Payments.Where(p => p.OperationId == request.OperationId).Include(o => o.ShippingAgent).ToListAsync();

        ICollection<OperationPaymentDto> reports = new List<OperationPaymentDto>();

        reports.Add(new OperationPaymentDto {
            Name = "Shipping Agent Fee",
            Data = from payment in payments where ShippingAgentPaymentType.Types.Contains(payment.Name) select payment,
        });

        reports.Add(new OperationPaymentDto {
            Name = "Terminal Port Fee",
            Data = from payment in payments where TerminalPortPaymentType.Types.Contains(payment.Name) select payment,
        });

        return reports;
    }
}