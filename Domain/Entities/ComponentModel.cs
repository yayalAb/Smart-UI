using Domain.Common;

namespace Domain.Entities
{
    public class ComponentModel: BaseAuditableEntity
    {
        public string ComponetName { get; set; }
        public string FormgroupName { get; set; }
        public int NoOfFeildsInRow { get; set; }
        public ICollection<feildsModel> Filds { get; set; } = null!;
    }
}