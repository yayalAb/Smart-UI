using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> entity) {
    
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.HasIndex(e => e.Id, "Id_UNIQUE")
            .IsUnique();

        entity.Property(e => e.OpenedDate)
            .HasColumnType("datetime");

        entity.Property(e => e.OperationNumber)
            .HasMaxLength(45);

        entity.Property(e => e.Status)
            .HasMaxLength(45);

        entity.Property(e => e.TruckId).HasColumnName("truck_id");

        entity.HasOne(d => d.BillOfLoading)
            .WithOne(p => p.Operation)
            .HasForeignKey<Operation>(d => d.BillOfLoadingId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_operation_Bill of Loading1");

        entity.HasOne(d => d.Company)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.CompanyId)
            .HasConstraintName("fk_operation_company1");

        entity.HasOne(d => d.Driver)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.DriverId)
            .HasConstraintName("fk_operation_driver1");

        entity.HasOne(d => d.Truck)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.TruckId)
            .HasConstraintName("fk_operation_truck1");

        // entity.HasOne(d => d.TerminalPortFee)
        //     .WithOne(p => p.Operation)
        //     .HasForeignKey<TerminalPortFee>(d => d.OperationId)
        //     .OnDelete(DeleteBehavior.ClientSetNull);
        
        // entity.HasOne(d => d.ShippingAgentFee)
        //     .WithOne(p => p.Operation)
        //     .HasForeignKey<ShippingAgentFee>(d => d.OperationId)
        //     .OnDelete(DeleteBehavior.ClientSetNull);
    
    }
}