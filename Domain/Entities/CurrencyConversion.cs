using Domain.Common;
namespace Domain.Entities;

public class CurrencyConversion : BaseAuditableEntity {

    public string Currency { get; set; }
    public Double Rate { get; set; }
    public DateTime Date { get; set; }

}