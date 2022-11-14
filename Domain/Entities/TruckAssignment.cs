

using Domain.Common;

namespace Domain.Entities
{
    public  class TruckAssignment : BaseAuditableEntity
    {
        public int DriverId { get; set; }   
        public int TruckId { get; set; }    
        public int OperationId { get; set; }
        public int SourcePortId { get; set; }
        public int DestinationPortId { get; set; }
        
        public virtual Driver Driver { get; set; }
        public virtual Truck Truck { get; set; }
        public virtual Operation Operation { get; set; }
        public virtual Port SourcePort { get; set; }    
        public virtual Port DestinationPort { get; set; }   

        public virtual ICollection<Container> Containers { get; set; }   

    }
}
