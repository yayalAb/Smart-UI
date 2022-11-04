namespace Domain.Entities;

public class Address
{
    public int Id { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string region { get; set; }
    public string city { get; set; }
    public string subcity { get; set; }
    public string country { get; set; }
    public string POBOX { get; set; }
}