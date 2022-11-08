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
        DbSet<BillOfLoading> BillOfLoadings { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<ContactPerson> ContactPeople { get; set; }
        DbSet<Container> Containers { get; set; }
        DbSet<Documentation> Documentations { get; set; }
        DbSet<Driver> Drivers { get; set; }
        DbSet<ECDDocument> ECDDocuments { get; set; }
        DbSet<Good> Goods { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Lookup> Lookups { get; set; }
        DbSet<Operation> Operations { get; set; }
        DbSet<Port> Ports { get; set; }
        DbSet<ShippingAgent> ShippingAgents { get; set; }
        DbSet<ShippingAgentFee> ShippingAgentFees { get; set; }
        DbSet<TerminalPortFee> TerminalPortFees { get; set; }
        DbSet<Truck> Trucks { get; set; }
        
        Task AddRangeAsync(IEnumerable<object> entities,CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
