
namespace Application.OperationDocuments.Queries.GoodsRemoval
{
    public class TruckAssignmentDto
    {
        public string? PlateNumber { get; set; } // truck
        public string? DriverName { get; set; } // driver
        public string? DriverPhone { get; set; } // driver
        public List<string>? ContainerNumbers { get; set; } // container
        public string? Quantity {get; set; } // 1x40 TruckAssignment - container
        public float? Weight { get; set; } // goods 
        public IEnumerable<string>? Description { get; set; } // goods

    }
}