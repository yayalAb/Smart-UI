using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> entity) {

        entity.Property(e => e.OpenedDate)
            .IsRequired(true)   
            .HasColumnType("datetime");

        entity.Property(e => e.OperationNumber)
            .IsRequired(true)
            .HasMaxLength(45);

        entity.Property(e => e.Status)
            .IsRequired(true    ) 
            .HasMaxLength(45);

        entity.Property(e => e.TruckId).HasColumnName("truck_id");

        entity.HasOne(d => d.BillOfLoading)
            .WithOne(p => p.Operation)
            .HasForeignKey<Operation>(d => d.BillOfLoadingId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Company)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.CompanyId);

        entity.HasOne(d => d.Driver)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.DriverId);


        entity.HasOne(d => d.Truck)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.TruckId);

    }
}