using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TruckConfiguration : IEntityTypeConfiguration<Truck> {
    public void Configure(EntityTypeBuilder<Truck> entity) {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.TruckNumber)
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .HasMaxLength(45);

        entity.HasOne(d => d.Image)
            .WithMany(p => p.Trucks)
            .HasForeignKey(d => d.ImageId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("image");
    }
}