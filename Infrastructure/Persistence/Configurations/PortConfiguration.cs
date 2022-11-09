using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PortConfiguration : IEntityTypeConfiguration<Port> {

    public void Configure(EntityTypeBuilder<Port> entity) {

        entity.Property(e => e.Country)
            .HasMaxLength(45);

        entity.Property(e => e.Name)
            .HasMaxLength(45);

        entity.Property(e => e.Region)
            .HasMaxLength(45);

        entity.Property(e => e.Vollume)
            .HasMaxLength(45);
        
    }

}