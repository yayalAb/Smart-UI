

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguaration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment>  entity)
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

            entity.HasOne(p => p.Operation)
                .WithMany(o => o.Payments)
                .HasForeignKey(d => d.OperationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(p => p.ShippingAgent)
                .WithMany(s => s.Payments)
                .HasForeignKey(d => d.ShippingAgentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
