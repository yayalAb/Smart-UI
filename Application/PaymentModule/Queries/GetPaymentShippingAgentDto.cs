using Application.Common.Mappings;
using Domain.Entities;

namespace Application.PaymentModule.Queries;
public class GetPaymentShippingAgentDto : IMapFrom<ShippingAgent>
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
}