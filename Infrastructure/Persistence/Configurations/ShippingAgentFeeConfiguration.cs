using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ShippingAgentFeeConfiguration : IEntityTypeConfiguration<ShippingAgentFee> {
    public void Configure(EntityTypeBuilder<ShippingAgentFee> entity)
    {
        entity.Property(e => e.BankCode)
            .HasMaxLength(45);

        entity.Property(e => e.Currency)
            .HasMaxLength(45);

        entity.Property(e => e.Description)
            .HasMaxLength(100);

        entity.Property(e => e.PaymentDate)
            .HasColumnType("datetime");

        entity.Property(e => e.PaymentMethod)
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .HasMaxLength(45);

        entity.HasOne(d => d.Operation)
            .WithOne(p => p.ShippingAgentFee)
            .HasForeignKey<ShippingAgentFee>(d => d.OperationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.Agent)
            .WithMany(p => p.AgentFees)
            .HasForeignKey(d => d.ShippingAgentId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}