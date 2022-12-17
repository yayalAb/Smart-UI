namespace Application.OperationDocuments.Queries.Number1
{
    public class Number1Dto
    {
        public DateTime? Date { get; set; } /// now
        public string? BillOfLoadingNumber { get; set; }
        public string? PortOfLoadingCountry { get; set; }
        public string? DefaultCompanyName { get; set; }
        public string? DefaultCompanyCodeNIF { get; set; }
        public string? SNumber { get; set; } // operation
        public DateTime? SDate { get; set; } //operation
        public string? DONumber { get; set; } // payment
        public DateTime? DODate { get; set; }  //payment
        public int? TotalNumberOfPackages { get; set; } // goods
        public string? RecepientName { get; set; }// operation
        public string? VesselName { get; set; } // operation
        public DateTime? ArrivalDate { get; set; } // operation
        public string? VoyageNumber { get; set; } // operation
        public string? ConnaissementNumber { get; set; } // operation
        public string? CountryOfOrigin { get; set; } // operation
        public float? REGTax { get; set; } // operation
        public ICollection<DocGoodDto> Goods { get; set; }
        public ICollection<No1ContainerDto> Containers { get; set; }
        public string? SourceLocation { get; set; }//??????????????
        public string? DestinationLocation { get; set; }//??????????


    }
}