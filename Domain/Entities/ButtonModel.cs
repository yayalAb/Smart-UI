using Domain.Common;

namespace Domain.Entities
{
    public class ButtonModel:BaseAuditableEntity
    {
        public string ComponetName { get; set; }="";
        public ICollection<ButtonFeilds> buttonData { get; set; } = null!;
    }
}

