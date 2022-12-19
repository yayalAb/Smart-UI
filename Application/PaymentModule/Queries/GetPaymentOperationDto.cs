using Application.Common.Mappings;
using Domain.Entities;

namespace Application.PaymentModule.Queries;
public class GetPaymentOperationDto : IMapFrom<Operation>
{
    public int Id { get; set; }
    public string OperationNumber { get; set; } = null!;
}