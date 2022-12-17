using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LookupConfiguration : IEntityTypeConfiguration<Lookup>
{

    public void Configure(EntityTypeBuilder<Lookup> entity)
    {

        entity.Property(e => e.Key)
            .HasMaxLength(45);

        entity.Property(e => e.Value)
            .HasMaxLength(45);

    }
}