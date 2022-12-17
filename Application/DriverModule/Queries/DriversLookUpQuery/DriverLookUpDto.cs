
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DriverModule.Queries.DriverLookUpQuery;

public class DriverLookUpDto : IMapFrom<Driver>
{
    public int Id { get; set; }
    public string Fullname { get; set; }
}