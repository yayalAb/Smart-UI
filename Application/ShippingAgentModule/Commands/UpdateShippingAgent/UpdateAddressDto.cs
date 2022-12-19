using Application.Common.Mappings;
using Domain.Entities;

namespace Application.ShippingAgentModule.Commands.UpdateShippingAgent
{
    public class UpdateAddressDto : IMapFrom<Address>
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Subcity { get; set; }
        public string? Country { get; set; }
        public string? POBOX { get; set; }
    }
}
