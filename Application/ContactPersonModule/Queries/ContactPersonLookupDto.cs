using Application.Common.Mappings;
using Domain.Entities;

namespace Application.ContactPersonModule.Queries;
public class ContactPersonLookupDto : IMapFrom<ContactPerson>{
    public int Id { get; set; }
    public  string Name { get; set; }
}