using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DriverModule;

public class DriverDto : IMapFrom<Driver> {
    public string Fullname { get; set; } = null!;
    public string LicenceNumber { get; set; } = null!;
    public int AddressId { get; set; }
    public byte[]? Image { get; set; }
    public int? TruckId { get; set; }
}