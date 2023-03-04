using Domain.Common;

namespace Domain.Entities
{
    public class TabModel : BaseAuditableEntity
    {
          public string TabName { get; set; }="";
          public string TabId { get; set; }="";
          public string ElementRef { get; set; }="";
          public int CompinentId { get; set; }
          public ComponentModel Component { get; set; } = null!;
          public int ProjectID {get; set;}
          public ProjectModel Project { get; set; } = null!;

    }


    }
