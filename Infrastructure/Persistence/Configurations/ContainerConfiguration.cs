using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ContainerConfiguration : IEntityTypeConfiguration<Container> {
    public void Configure(EntityTypeBuilder<Container> entity) {

        entity.Property(e => e.ContianerNumber)
            .HasMaxLength(45);

        entity.Property(e => e.Loacation)
            .HasMaxLength(45);

        entity.Property(e => e.Owner)
            .HasMaxLength(45);

        
    }
}