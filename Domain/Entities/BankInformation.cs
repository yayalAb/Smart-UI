using Domain.Common;

namespace Domain.Entities;
public class BankInformation : BaseAuditableEntity {

    public string AccountHolderName { get; set; } = null!;
    public string BankName { get; set; } = null!;
    public string AccountNumber { get; set; } = null!;
    public string SwiftCode { get; set; } = null!;
    public string BankAddress { get; set; } = null!;
    public int CompanyId { get; set; }
    public string CurrencyType { get; set; }
    public virtual Company Company {get; set; } = null!; 
}