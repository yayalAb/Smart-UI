
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Number9.N9Dtos;

public class N9NameOnPermitDto : IMapFrom<ContactPerson>
{
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public string TinNumber { get; set; } = null!;
}