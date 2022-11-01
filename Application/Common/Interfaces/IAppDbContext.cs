using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DatabaseFacade database { get; }
        DbSet<UserRole> UserRoles { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
