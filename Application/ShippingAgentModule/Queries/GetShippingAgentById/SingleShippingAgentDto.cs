using System.Reflection.Metadata;
using Application.Common.Mappings;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Domain.Entities;

namespace Application.ShippingAgentModule.Queries.GetShippingAgentById{
    public class SingleShippingAgentDto :IMapFrom<ShippingAgent>{
        public int Id {get; set;}
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public int AddressId {get; set;}
        public string? Image { get; set; }
         public UpdateAddressDto Address {get; set;}


    }
}