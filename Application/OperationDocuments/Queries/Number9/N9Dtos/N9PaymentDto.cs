using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Number9.N9Dtos;

public class N9PaymentDto : IMapFrom<Payment>
{
    public DateTime PaymentDate { get; set; }
    public string? DONumber { get; set; }
}