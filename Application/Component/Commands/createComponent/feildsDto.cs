using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Component.Commands.createComponent
{
    public class feildsDto:IMapFrom<feildsModel>
    {
        public string feildType { get; set; }
        public string labelName { get; set; }
        public string elementType { get; set; }
        public string formControlName { get; set; }
        public string className { get; set; }
        public string validators { get; set; }
        public int minLength { get; set; }
        public int maxLength { get; set; }=100;
    }
}