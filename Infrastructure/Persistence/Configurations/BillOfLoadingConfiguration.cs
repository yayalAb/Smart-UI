using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BillOfLoadingConfiguration : IEntityTypeConfiguration<BillOfLoading>
{
    public void Configure(EntityTypeBuilder<BillOfLoading> entity) {

        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.HasIndex(e => e.Id, "Id_UNIQUE")
            .IsUnique();

        entity.Property(e => e.ActualDateOfDeparture)
            .HasColumnType("datetime");

        entity.Property(e => e.ATA)
            .HasMaxLength(45);

        entity.Property(e => e.BillNumber)
            .HasMaxLength(45);

        entity.Property(e => e.Consignee)
            .HasMaxLength(45);

        entity.Property(e => e.CustomerName)
            .HasMaxLength(45);

        entity.Property(e => e.DestinationType)
            .HasMaxLength(45);

        entity.Property(e => e.EstimatedTimeOfArrival)
            .HasColumnType("datetime");

        entity.Property(e => e.FZIN)
            .HasMaxLength(45);

        entity.Property(e => e.FZOUT)
            .HasMaxLength(45);

        entity.Property(e => e.GoodsDescription)
            .HasMaxLength(255);

        entity.Property(e => e.GrossWeight).HasColumnName("grossWeight");

        entity.Property(e => e.NameOnPermit)
            .HasMaxLength(45);

        entity.Property(e => e.NotifyParty)
            .HasMaxLength(45);

        entity.Property(e => e.ShippingAgent)
            .HasMaxLength(45);

        entity.Property(e => e.ShippingLine)
            .HasMaxLength(45);

        entity.Property(e => e.TruckNumber)
            .HasMaxLength(45);

        entity.Property(e => e.TypeOfMerchandise)
            .HasMaxLength(45);

        entity.Property(e => e.VoyageNumber)
            .HasMaxLength(45);

        entity.HasOne(d => d.Container)
            .WithMany(p => p.BillOfLoadings)
            .HasForeignKey(d => d.ContainerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_Bill of Loading_container1");

        entity.HasOne(d => d.Port)
            .WithMany(p => p.BillOfLoadings)
            .HasForeignKey(d => d.PortId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_Bill of Loading_port1");

        entity.HasOne(d => d.Operation)
            .WithOne(p => p.BillOfLoading)
            .HasForeignKey<Operation>(d => d.BillOfLoadingId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}