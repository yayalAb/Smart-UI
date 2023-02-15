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
        
        
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
