

using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TerminalPortFeeModule.Queries.GetTerminalPortFeeById
{
    public class TerminalPortFeeDto : IMapFrom<TerminalPortFee>
    {
        public int Id { get; set; }
        public string Type { get; set; } 
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } 
        public string? BankCode { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
        public string? Description { get; set; }
        public int OperationId { get; set; }
    }
}
