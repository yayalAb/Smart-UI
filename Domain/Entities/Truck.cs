namespace Domain.Entities;

public class Truck {

    public Truck() {
        Operations = new HashSet<Operation>();
    }

    public int Id { get; set; }
    public string TruckNumber { get; set; } = null!;
    public string Type { get; set; } = null!;
    public float Capacy { get; set; }
    public int ImageId { get; set; }
    
    //has one
    public Image Image { get; set; } = null!;
    
    //has many
    public ICollection<Operation> Operations { get; set; }
}