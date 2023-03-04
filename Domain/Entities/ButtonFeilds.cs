using Domain.Common;

namespace Domain.Entities
{
    public class ButtonFeilds:BaseAuditableEntity
    {
        public string buttonType { get; set; }="";
        public string color { get; set; }="";
        public string className { get; set; }="";
        public string buttonName { get; set; }="";
        public string eventName { get; set; }="";
        public int  buttonId  { get; set; }
        public virtual ButtonModel button  { get; set; } = null!;

        
    }
}
