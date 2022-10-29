using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DatabaseFacade database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
