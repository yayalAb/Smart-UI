using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image> {
    
    public void Configure(EntityTypeBuilder<Image> entity) {

        entity.HasOne(d => d.ShippingAgent)
           .WithOne(p => p.Image)
           .HasForeignKey<ShippingAgent>(d => d.ImageId)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Driver)
          .WithOne(p => p.Image)
          .HasForeignKey<Driver>(d => d.ImageId)
          .IsRequired(false)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Container)
          .WithOne(p => p.Image)
          .HasForeignKey<Container>(d => d.ImageId)
          .IsRequired(false)
          .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Truck)
          .WithOne(p => p.Image)
          .HasForeignKey<Truck>(d => d.ImageId)
          .IsRequired(false)
          .OnDelete(DeleteBehavior.ClientSetNull);

    }
}