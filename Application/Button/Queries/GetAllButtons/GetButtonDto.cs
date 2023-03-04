using Application.Button.Commands.CreateButtonCommand;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Button.Queries.GetAllButtons
{
    public class GetButtonDto:IMapFrom<ButtonModel>
    {
        public string ComponetName { get; init; }
        public ICollection<ButtonFeildsDto> buttonData { get; init; }
    }
}