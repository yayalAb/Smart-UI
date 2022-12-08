
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.OperationDocuments.Number9.N9Dtos;

public class N9CompanyDto : IMapFrom<Company> {
    public string? Name { get; set; }
    public string? TinNumber { get; set; }
    public string? CodeNIF { get; set; }
    public int ContactPersonId { get; set; }
    public virtual N9NameOnPermitDto ContactPerson { get; set; } = null!;
}