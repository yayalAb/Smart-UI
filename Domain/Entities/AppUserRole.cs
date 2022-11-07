using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Domain.Entities
{
    public class AppUserRole : BaseAuditableEntity
    {
        public string Page { get; set; }    
        public string Title { get; set; }
        public bool CanAdd { get; set; } = true;
        public bool CanDelete { get; set; } = true;
        public bool CanViewDetail { get; set; } = true;
        public bool CanView { get; set; } = true;
        public bool CanUpdate { get; set; } = true; 
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
                    Title = pageTitle,
                    Page = pageName,
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
                if (!userRoles.Any(r => r.Page.ToLower() == page.ToLower()))
                {
                    string title = page.Replace("_", " ");
                    userRoles.Add(new AppUserRole
                    {
                        Title = title,
                        Page = page,

                    });

                }
            }
  
            return userRoles;
        }
       
    }
}
