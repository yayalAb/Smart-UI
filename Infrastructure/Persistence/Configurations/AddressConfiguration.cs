using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{

    public void Configure(EntityTypeBuilder<Address> entity)
    {
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.City)
            .HasMaxLength(45);

        entity.Property(e => e.Country)
            .HasMaxLength(45);

        entity.Property(e => e.Email)
            .HasMaxLength(45);

        entity.Property(e => e.Phone)
            .HasMaxLength(45);

        entity.Property(e => e.POBOX)
            .HasMaxLength(45);

        entity.Property(e => e.Region)
            .HasMaxLength(45);

        entity.Property(e => e.Subcity)
            .HasMaxLength(45);

        entity.HasOne<ApplicationUser>()
            .WithOne(u => u.Address)
            .HasForeignKey<ApplicationUser>(u => u.AddressId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }

}