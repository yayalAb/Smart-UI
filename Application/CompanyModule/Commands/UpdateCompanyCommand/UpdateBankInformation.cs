using Application.Common.Mappings;
using Domain.Entities;

namespace Application.CompanyModule.Commands.UpdateCompanyCommand;
public class UpdateBankInformationDto : IMapFrom<BankInformation>
{
    public int Id { get; set; }
    public string AccountHolderName { get; set; }
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string SwiftCode { get; set; } 
    public string BankAddress { get; set; }
    public string CurrencyType { get; set; }
}