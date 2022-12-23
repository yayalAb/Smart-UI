using Application.ContainerModule;
using Application.OperationDocuments.Number9.N9Dtos;
using Application.OperationDocuments.Queries;

namespace Application.OperationDocuments.Queries.Number9Transfer;
public class TransferNumber9Dto
{
    public string? defaultCompanyName { get; set; }
    public string? defaultCompanyCodeNIF { get; set; }
    public N9PortOfLoadingDto DestinationPort { get; set ;}
    public N9CompanyDto company {get; set;}
    public N9OperationDto operation {get; set;}
    public N9PaymentDto? doPayment {get; set;}
    public List<DocGoodDto> goods {get; set;}
    public List<ContainerDto> containers { get; set; }
    public Double TotalWeight { get; set; }
    public Double TotalPrice { get; set; }
    public int TotalQuantity { get; set; }
    public string WeightUnit {get; set;}
    public string Currency {get; set;}
}