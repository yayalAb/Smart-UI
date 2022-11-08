using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ShippingAgentConfiguration : IEntityTypeConfiguration<ShippingAgent> {
    public void Configure(EntityTypeBuilder<ShippingAgent> entity) {

        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.HasIndex(e => e.Id, "id_UNIQUE")
            .IsUnique();

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.FullName)
            .HasMaxLength(45);

        entity.Property(e => e.CompanyName)
            .HasMaxLength(45);

        entity.HasOne(d => d.Address)
            .WithOne(p => p.ShippingAgent)
            .HasForeignKey<ShippingAgent>(d => d.AddressId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_shipping agent_address1");

        entity.HasOne(d => d.Image)
            .WithMany(p => p.ShippingAgents)
            .HasForeignKey(d => d.ImageId)
            .HasConstraintName("fk_shipping agent_image1");

    }
}