
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.SettingModule.Command.UpdateSettingCommand;

public class CompanyUpdateDto {
    public string? Name { get; set; }
    public string? TinNumber { get; set; }
    public string? CodeNIF { get; set; }
    public int ContactPersonId { get; set; }
    public int AddressId { get; set; }
    public AddressUpdateDto Address {get; set;}
    public ICollection<BankInformationUpdateDto> BankInformation {get; set;}
}