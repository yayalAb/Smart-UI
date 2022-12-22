
using Application.OperationDocuments.Number9.N9Dtos;

namespace Application.OperationDocuments.Queries.Number9;

public class Number9Dto
{
    public string? defaultCompanyName { get; set; }
    public string? defaultCompanyCodeNIF { get; set; }
    public N9CompanyDto company { get; set; }
    public N9OperationDto operation { get; set; }
    public N9PaymentDto doPayment { get; set; }
    public ICollection<N9ContainerDto> container { get; set; }
    public ICollection<N9GoodDto> goods { get; set; }

}