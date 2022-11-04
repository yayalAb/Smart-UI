namespace Domain.Entities;

public class BillOfLoading {
    public int Id { get; set; }
    public string customerName { get; set; }
    public string nameOnPermit { get; set; }
    public string consignee { get; set; }
    public string notifyParty { get; set; }
    public string billNumber { get; set; }
    public string shippingLine { get; set; }
    public string goodsDescription { get; set; }
    public float quantity { get; set; }
    public int containerId { get; set; }
    public float grossWeight { get; set; }
    public string truckNumber { get; set; }
    public string ATA { get; set; }
    public string FZIN { get; set; }
    public string FZOUT { get; set; }
    public string destinationType { get; set; }
    public string shippingAgent { get; set; }
    public int portOfLoading { get; set; }
    public DateTime actualDateOfDeparture { get; set; }
    public DateTime estimatedDateOfArrival { get; set; }
    public string voyageNumber { get; set; }
    public string typeOfMerchandise { get; set; }
    public int port_Id { get; set; }
    public int container_id { get; set; }
    
}