
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserGroupConfigurations : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasMany<ApplicationUser>()
                .WithOne(u=>u.UserGroup)
                .HasForeignKey(u=>u.UserGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
            builder.HasIndex(u=>u.Name).IsUnique();

        }
    }
}
