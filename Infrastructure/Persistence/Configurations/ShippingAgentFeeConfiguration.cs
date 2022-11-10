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
            .IsRequired()   
            .HasMaxLength(45);

        entity.Property(e => e.Description)
            .HasMaxLength(100);

        entity.Property(e => e.PaymentDate)
            .IsRequired()    
            .HasColumnType("datetime");

        entity.Property(e => e.PaymentMethod)
            .IsRequired()  
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(45);

        entity.Property(e => e.Amount)
            .IsRequired();

        entity.HasOne(d => d.Operation)
            .WithMany(p => p.ShippingAgentFees)
            .HasForeignKey(d => d.OperationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.Agent)
            .WithMany(p => p.AgentFees)
            .HasForeignKey(d => d.ShippingAgentId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}