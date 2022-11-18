using Application.Common.Mappings;
using Domain.Entities;

namespace Application.CompanyModule.Queries;
public class ContactPersonDto : IMapFrom<ContactPerson>
{
    public int Id {get; set;}
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}