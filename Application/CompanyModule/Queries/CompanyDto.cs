using Application.Common.Mappings;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using Domain.Entities;

namespace Application.CompanyModule.Queries;
public class CompanyDto : IMapFrom<Company>
{
    public int Id {get; set;}
    public string? Name { get; set; }
    public string? TinNumber { get; set; }
    public string? CodeNIF { get; set; }
    public int ContactPersonId { get; set; }
    public int AddressId { get; set; }
    
    public virtual ContactPersonDto ContactPerson { get; set; } = null!;
    public virtual AddressDto Address { get; set; } = null!;
}