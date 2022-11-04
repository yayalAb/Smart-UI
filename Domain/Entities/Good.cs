namespace Domain.Entities;

public class Good
{
    public int Id { get; set; }
    public string description { get; set; }
    public string HSCode { get; set; }
    public string manufacturer { get; set; }
    public string CBM { get; set; }
    public float weight { get; set; }
    public float quantity { get; set; }
    public float unitPrice { get; set; }
    public float unitOfMeasurnment { get; set; }
    public string goodcol { get; set; }
    public int containerId { get; set; }
    
}