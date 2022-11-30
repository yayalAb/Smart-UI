namespace Application.OperationDocuments.Queries.GoodsRemoval
{
    public class GoodsRemovalDto
    {
        public DateTime Date { get; set; } // now
        public string? Declarant { get; set; } //?
        public string? ImporterName { get; set; } //? client / company name?
        public string? ShippingAgentName { get; set; } // operation
        public string? BillOfloadingNumber { get; set; } // operation
        public string? REFNumber { get; set; } //?
        public string? ClearanceOffice { get; set; }//?
        public string? FrontierOffice { get; set; }//?
        public string? LocationOfLoading { get; set; } // operation portofloading > port country
        public string? VoyageNumber { get; set; } // operation
        public string? DeclarationNumber { get; set; } //?
        public string? FinalDestination { get; set; } //?
        public List<TruckAssignmentDto> TruckAssignments { get; set; }


    }
}