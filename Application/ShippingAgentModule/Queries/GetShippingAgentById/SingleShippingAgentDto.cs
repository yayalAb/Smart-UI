using Application.Common.Mappings;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Domain.Entities;

namespace Application.ShippingAgentModule.Queries.GetShippingAgentById{
    public class SingleShippingAgentDto :IMapFrom<ShippingAgent>{
        public int Id {get; set;}
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public int AddressId {get; set;}
        public byte[]? Image { get; set; }
         public AddressDto Address {get; set;}


    }
}