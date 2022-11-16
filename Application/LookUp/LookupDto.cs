using Application.Common.Mappings;
using Domain.Entities;

namespace Application.LookUp;

public class LookupDto : IMapFrom<Lookup> {
    public int Id {get; set;}
    public string Key {get; set;}
    public string Value {get; set;}
}