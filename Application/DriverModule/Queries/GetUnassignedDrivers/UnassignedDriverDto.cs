using Application.Common.Mappings;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Domain.Entities;

namespace Application.DriverModule.Queries.GetUnassignedDrivers;
public class UnassignedDriverDto : IMapFrom<Driver>
{
    public int Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string LicenceNumber { get; set; } = null!;
    public int AddressId { get; set; }
    public bool IsAssigned { get; set; }
    public UpdateAddressDto Address { get; set; }

}