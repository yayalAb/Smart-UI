namespace Domain.Entities;

public class ContactPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    //has one
    public virtual Company Company { get; set; } = null!;
}