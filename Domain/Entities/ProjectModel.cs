using Domain.Common;

namespace Domain.Entities
{
    public class ProjectModel : BaseAuditableEntity
    {
          public string ProjectName { get; set; }="";
          public string Description { get; set; }="";
    }
}