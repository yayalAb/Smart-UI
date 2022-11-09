using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TerminalPortFeeConfiguration : IEntityTypeConfiguration<TerminalPortFee> {
    
    public void Configure(EntityTypeBuilder<TerminalPortFee> entity) {

        entity.Property(e => e.BankCode)
            .HasMaxLength(45);

        entity.Property(e => e.Currency)
            .IsRequired()
            .HasMaxLength(45);

        entity.Property(e => e.Description)
            .HasMaxLength(100);

        entity.Property(e => e.OperationId).HasColumnName("operation_Id");

        entity.Property(e => e.PaymentDate)
            .IsRequired()
            .HasColumnType("datetime");

        entity.Property(e => e.PaymentMethod)
            .IsRequired()
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(45);

        entity.HasOne(d => d.Operation)
            .WithMany(p => p.TerminalPortFees)
            .HasForeignKey(d => d.OperationId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
    }
}