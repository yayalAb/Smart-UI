
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.SettingModule.Command.UpdateSettingCommand;

public class AddressUpdateDto : IMapFrom<Address> {
    public int Id {get; set;}
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Subcity { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? POBOX { get; set; }
}