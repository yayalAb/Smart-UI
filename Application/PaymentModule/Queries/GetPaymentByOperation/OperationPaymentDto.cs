
using Domain.Entities;

namespace Application.PaymentModule.Queries.GetPaymentByOperation;

public class OperationPaymentDto {
    public IEnumerable<Payment> ShippingAgnetFee {get; set;}
    public IEnumerable<Payment> TerminalPortFee {get; set;}
}