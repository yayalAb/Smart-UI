using Application.Common.Mappings;
using Application.CompanyModule.Commands.UpdateCompanyCommand;
using Application.ShippingAgentModule.Commands.UpdateShippingAgent;
using Domain.Entities;

namespace Application.CompanyModule.Queries;
public class CompanyDto : IMapFrom<Company>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? TinNumber { get; set; }
    public string? CodeNIF { get; set; }
    public int AddressId { get; set; }

    public virtual ICollection<ContactPersonDto> ContactPeople { get; set; } = null!;
    public virtual UpdateAddressDto Address { get; set; } = null!;
    public virtual ICollection<UpdateBankInformationDto> BankInformation { get; set; }

}