using Application.Common.Mappings;
using Application.Component.Commands.createComponent;
using Domain.Entities;

namespace Application.Component.Queries
{
    public class ComponentDto: IMapFrom<ComponentModel>
    {
        public string ComponetName { get; set; }="";
        public string FormgroupName { get; set; }="";
        public int NoOfFeildsINRow { get; set; }=1;
        public ICollection<feildsDto> Filds { get; init; }
    }
}