using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ShippingAgentConfiguration : IEntityTypeConfiguration<ShippingAgent> {
    public void Configure(EntityTypeBuilder<ShippingAgent> entity) {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(45);

        entity.Property(e => e.CompanyName)
            .HasMaxLength(45);

        entity.HasOne(d => d.Address)
            .WithOne(p => p.ShippingAgent)
            .HasForeignKey<ShippingAgent>(d => d.AddressId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}