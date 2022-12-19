
using Domain.Entities;

namespace Application.PaymentModule.Queries.GetPaymentByOperation;

public class OperationPaymentDto
{
    public string Name { get; set; }
    public IEnumerable<Payment> Data { get; set; }

}