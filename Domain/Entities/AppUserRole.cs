using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Domain.Entities
{
    public class AppUserRole : BaseAuditableEntity
    {
        public string page { get; set; }    
        public string title { get; set; }
        public bool canAdd { get; set; } = true;
        public bool canDelete { get; set; } = true;
        public bool canViewDetail { get; set; } = true;
        public bool canView { get; set; } = true;
        public bool canUpdate { get; set; } = true; 
        public string ApplicationUserId { get; set; }
      

        public static List<AppUserRole> createDefaultRoles(string userId)
        {
            
            List<AppUserRole> defaultRoles = new List<AppUserRole>();
            foreach (string page in Enum.GetNames(typeof(Page)))
            {
                var pageTitle = page.Replace("_", " ");
                var pageName = page;
                defaultRoles.Add(new AppUserRole
                {
                    title = pageTitle,
                    page = pageName,
                    ApplicationUserId = userId
                });
            }
            return defaultRoles;
        }
        public static List<AppUserRole> fillUndefinedRoles(List<AppUserRole> userRoles)
        {
            foreach (string page in Enum.GetNames(typeof(Page)))
            {
                // if userRole with the page is not found fill it with default one
                if (!userRoles.Any(r => r.page.ToLower() == page.ToLower()))
                {
                    string title = page.Replace("_", " ");
                    userRoles.Add(new AppUserRole
                    {
                        title = title,
                        page = page,

                    });

                }
            }
  
            return userRoles;
        }
       
    }
}
