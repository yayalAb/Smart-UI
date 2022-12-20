using Application.ContainerModule;
using Application.OperationDocuments.Number9.N9Dtos;

namespace Application.OperationDocuments.Number9Transfer;
public class TransferNumber9Dto
{
    public string? defaultCompanyName { get; set; }
    public string? defaultCompanyCodeNIF { get; set; }
    public N9PortOfLoadingDto DestinationPort { get; set ;}
    public N9CompanyDto company {get; set;}
    public N9OperationDto operation {get; set;}
    public N9PaymentDto? doPayment {get; set;}
    public List<N9GoodDto> goods {get; set;}
    public List<ContainerDto> containers { get; set; } 
}