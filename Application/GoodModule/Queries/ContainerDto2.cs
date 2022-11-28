using Application.Common.Mappings;
using Domain.Entities;

namespace Application.GoodModule.Queries;

public class ContainerDto2 : IMapFrom<Container> {
    public int Id { get; set; }
    public string ContianerNumber { get; set; }

}