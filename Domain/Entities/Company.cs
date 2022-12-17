using Domain.Common;
namespace Domain.Entities;

public class Company : BaseAuditableEntity
{

    public Company()
    {
        Operations = new HashSet<Operation>();
        BankInformation = new HashSet<BankInformation>();

    }
    public string? Name { get; set; }
    public string? TinNumber { get; set; }
    public string? CodeNIF { get; set; }
    // public int ContactPersonId { get; set; }
    public int AddressId { get; set; }

    public virtual ICollection<ContactPerson> ContactPeople { get; set; } = null!;
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<Setting> DefaultSetting { get; set; } = null!;
    public virtual ICollection<BankInformation> BankInformation { get; set; }

    public ICollection<Operation> Operations { get; set; }

}