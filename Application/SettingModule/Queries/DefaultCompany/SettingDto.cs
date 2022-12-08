using Application.Common.Mappings;
using Application.SettingModule.Command.UpdateSettingCommand;
using Domain.Entities;

namespace Application.SettingModule.Queries.DefaultCompany;

public class SettingDto : IMapFrom<Setting> {
    public int Id {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public int Port {get; set;}
    public string Host {get; set;}
    public string Protocol {get; set;}
    public string Username {get; set;}
    public int CompanyId {get; set;}

    public CompanyUpdateDto DefaultCompany {get; set;} = null!;
    public AddressUpdateDto Address {get; set;}
    public BankInformationUpdateDto BankInformation {get; set;}
}