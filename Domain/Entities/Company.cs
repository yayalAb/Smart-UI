namespace Domain.Entities;

public class Company {
    
    public Company()
    {
        Operations = new HashSet<Operation>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? TinNumber { get; set; }
    public string? CodeNIF { get; set; }
    public int ContactPersonId { get; set; }
    public int AddressId { get; set; }
    
    public virtual ContactPerson ContactPerson { get; set; } = null!;
    public virtual Address Address { get; set; } = null!;
    
    public ICollection<Operation> Operations { get; set; }
    
}