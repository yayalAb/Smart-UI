namespace Application.OperationDocuments.Queries.CommercialInvoice
{
    public class CIGoodsDto
    {
        public string? Description { get; set; }
        public string? HSCode { get; set; }
        public float? Quantity { get; set; }
        public string? Unit { get; set; }
        public float? UnitPrice { get; set; }
        public string? ContainerNumber { get; set; }

    }
}