

using Application.Common.Mappings;
using Domain.Entities;

namespace Application.ShippingAgentModule.Commands.CreateShippingAgent
{
    public  class AddressDto : IMapFrom<Address>
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Subcity { get; set; }
        public string? Country { get; set; }
        public string? POBOX { get; set; }
    }
}
