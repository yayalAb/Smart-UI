using Domain.Common;

namespace Domain.Entities
{
    public class feildsModel: BaseAuditableEntity
    {
        public string feildType { get; set; }
        public string labelName { get; set; }
        public string elementType { get; set; }
        public string formControlName { get; set; }
        public string className { get; set; }
        public string validators { get; set; }
        public int minLength { get; set; }
        public int maxLength { get; set; }
        public int  ComponentId  { get; set; }
        public virtual ComponentModel Component  { get; set; } = null!;
    }
}

