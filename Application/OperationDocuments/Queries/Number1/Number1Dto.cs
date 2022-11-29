using Application.GoodModule;
using Application.GoodModule.Queries;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Number1
{
    public class Number1Dto
    {
        public DateTime Date { get; set;} /// now
        public string? CodeNIF { get; set; }  // company
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
        public ICollection<DocGoodDto> Goods { get; set;  }
        public string? SourceLocation { get; set; }//??????????????
        public string? DestinationLocation { get; set; }//??????????
        

    }
}