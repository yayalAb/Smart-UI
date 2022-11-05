namespace Domain.Entities;

public class ShippingAgent
{
    public int Id { get; set; }
    public string fullName { get; set; }
    public int imageId { get; set; }
    public int addressId { get; set; }
    
    public Image agentImage { get; set; }
    public Address agentAddress { get; set; }
}