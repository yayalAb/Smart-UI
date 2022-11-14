using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TruckConfiguration : IEntityTypeConfiguration<Truck> {
    public void Configure(EntityTypeBuilder<Truck> entity) {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
 
        entity.Property(e => e.TruckNumber)
            .IsRequired()
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(45);

    }
}