using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address> {

    public void Configure(EntityTypeBuilder<Address> entity) {
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
    
    }

}