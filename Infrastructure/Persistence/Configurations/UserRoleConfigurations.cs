
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public  class UserRoleConfigurations : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasOne<ApplicationUser>()
                .WithMany(u => u.UserRoles).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
