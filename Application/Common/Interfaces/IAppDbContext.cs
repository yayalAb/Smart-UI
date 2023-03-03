using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DatabaseFacade database { get; }
        DbSet<AppUserRole> AppUserRoles { get; }
        DbSet<UserGroup> UserGroups { get; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Blacklist> Blacklists { get; set; }
        DbSet<Lookup> Lookups { get; set; }
        DbSet<TabsModel> Tabs{ get; set; }
        DbSet<ProjectModel> Projects { get; set; }
        DbSet<ComponentModel> Components { get; set; }
        DbSet<feildsModel> feilds { get; set; }
        DbSet<ButtonModel> buttons { get; set; }
        
 
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
