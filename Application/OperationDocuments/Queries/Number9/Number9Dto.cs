
using Application.OperationDocuments.Number9.N9Dtos;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.Number9;

public class Number9Dto {
    public N9CompanyDto company {get; set;}
    public N9OperationDto operation {get; set;}
    public N9PaymentDto doPayment {get; set;}
    public ICollection<N9GoodDto> goods {get; set;}
    // public IEnumerable<float> containerSize {get; set;}
}