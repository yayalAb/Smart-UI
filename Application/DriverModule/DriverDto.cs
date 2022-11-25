using Application.Common.Mappings;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Domain.Entities;

namespace Application.DriverModule;

public class DriverDto : IMapFrom<Driver> {
    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string LicenceNumber { get; set; } = null!;
    public int AddressId { get; set; }
    public byte[]? Image { get; set; }
    public bool IsAssigned { get; set; }
    public AddressDto Address {get; set;}

}