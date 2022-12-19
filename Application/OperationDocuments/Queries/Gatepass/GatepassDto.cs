namespace Application.OperationDocuments.Queries.Gatepass
{
    public class GatepassDto
    {
        public DateTime Date { get; set; }
        public string OperationNumber { get; set; }
        public string TruckNumber { get; set; }
        public float Quantity { get; set; }
        public float Weight { get; set; }
        public IEnumerable<string> Descriptions { get; set; }
        public IEnumerable<string> ContainerNumbers { get; set; }
        public string Localization { get; set; }
    }
}