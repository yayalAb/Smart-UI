namespace Domain.Entities;

public class Driver {
    public int Id { get; set; }
    public string fullName { get; set; }
    public string liceneceNumber { get; set; }
    public int addressId { get; set; }
    public int imageId { get; set; }
    public int truckId { get; set; }
    
    public Address driverAddress { get; set; }
    public Truck driverTruck { get; set; }
    public Image profileImage { get; set; }
}