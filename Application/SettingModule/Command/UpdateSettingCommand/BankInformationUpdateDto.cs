
namespace Application.SettingModule.Command.UpdateSettingCommand;
public class BankInformationUpdateDto {
    public string AccountHolderName { get; set; } = null!;
    public string BankName { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
    public string SwiftCode { get; set; } = null!;
    public string BankAddress { get; set; } = null!;
    public int CompanyId { get; set; }
}