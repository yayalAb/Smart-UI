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
        DbSet<Company> Companies { get; set; }
        DbSet<ContactPerson> ContactPeople { get; set; }
        DbSet<Container> Containers { get; set; }
        DbSet<Documentation> Documentations { get; set; }
        DbSet<Driver> Drivers { get; set; }
        DbSet<Good> Goods { get; set; }
        DbSet<Lookup> Lookups { get; set; }
        DbSet<Operation> Operations { get; set; }
        DbSet<OperationStatus> OperationStatuses { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Port> Ports { get; set; }
        DbSet<ShippingAgent> ShippingAgents { get; set; }
        DbSet<Truck> Trucks { get; set; }
        DbSet<TruckAssignment> TruckAssignments { get; set; }
        DbSet<Setting> Settings {get; set;}
        
        Task AddRangeAsync(IEnumerable<object> entities,CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
