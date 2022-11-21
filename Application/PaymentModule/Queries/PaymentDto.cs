
using Application.Common.Mappings;
using Application.OperationModule.Queries;
using Application.ShippingAgentModule.Queries;
using Domain.Entities;

namespace Application.PaymentModule.Queries
{
    public class PaymentDto : IMapFrom<Payment>
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Type { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string? BankCode { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        public string? Description { get; set; }
        public int OperationId { get; set; }
        public int ShippingAgentId { get; set; }
        public GetPaymentOperationDto Operation{get; set;}
        public GetPaymentShippingAgentDto ShippingAgent {get; set;}
    }
}
