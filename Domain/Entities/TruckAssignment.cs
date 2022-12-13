using Domain.Common;

namespace Domain.Entities
{
    public  class TruckAssignment : BaseAuditableEntity
    {
        
        public int DriverId { get; set; }   
        public int TruckId { get; set; }    
        public int OperationId { get; set; }
        public string SourceLocation {get; set; }
        public string DestinationLocation { get; set; }
        public int? SourcePortId { get; set; }
        public int? DestinationPortId { get; set; }
        public string? TransportationMethod { get; set; }
        public float AgreedTariff { get; set; }
        public string SENumber { get; set; }
        public DateTime Date { get; set; }
        public string GatePassType { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Truck Truck { get; set; }
        public virtual Operation Operation { get; set; }
        public virtual Port SourcePort { get; set; }    
        public virtual Port DestinationPort { get; set; }   

        public virtual ICollection<Container>? Containers { get; set; }   
        public virtual ICollection<Good>? Goods { get; set; }

    }
}
