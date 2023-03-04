using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Button.Commands.CreateButtonCommand
{
    public class ButtonFeildsDto:IMapFrom<ButtonFeilds>
    {
        public string buttonType { get; set; }="";
        public string color { get; set; }="";
        public string className { get; set; }="";
        public string buttonName { get; set; }="";
        public string eventName { get; set; }="";
    }
}