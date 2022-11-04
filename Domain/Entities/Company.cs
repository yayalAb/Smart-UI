namespace Domain.Entities;

public class Company {
    public int Id { get; set; }
    public string name { get; set; }
    public string tinNumber { get; set; }
    public string codeNIF { get; set; }
    public int contactPersonId { get; set; }
    public int addressId { get; set; }
    
    public ContactPerson contactPerson { get; set; }
    public Address companyAddress { get; set; }
}