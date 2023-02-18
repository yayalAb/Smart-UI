using Domain.Common;

namespace Domain.Entities
{
    public class TabsModel : BaseAuditableEntity
    {
        public string TabName { get; set; }="";
        public string TabContent { get; set; }="";
        
    }
}