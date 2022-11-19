using Application.Common.Mappings;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Domain.Entities;

namespace Application.ShippingAgentModule.Queries{
    public class ShippingAgentDto {
        public int Id {get; set;}
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public byte[]? Image { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Country { get; set; }

    }
}