using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LookupConfiguration : IEntityTypeConfiguration<Lookup> {
    
    public void Configure(EntityTypeBuilder<Lookup> entity) {

        entity.Property(e => e.Name)
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .HasMaxLength(45);

    }
}