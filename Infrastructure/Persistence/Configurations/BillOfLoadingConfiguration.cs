using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BillOfLoadingConfiguration : IEntityTypeConfiguration<BillOfLoading>
{
    public void Configure(EntityTypeBuilder<BillOfLoading> entity) {

        entity.Property(e => e.ActualDateOfDeparture)
            .HasColumnType("datetime");

        entity.Property(e => e.ATA)
            .HasMaxLength(45);

        entity.Property(e => e.BillNumber)
            .IsRequired(true)   
            .HasMaxLength(45);

        entity.Property(e => e.Consignee)
            .HasMaxLength(45);

        entity.Property(e => e.CustomerName)
            .IsRequired(true)
            .HasMaxLength(45);

        entity.Property(e => e.DestinationType)
            .IsRequired(true)    
            .HasMaxLength(45);

        entity.Property(e => e.EstimatedTimeOfArrival)
            .HasColumnType("datetime");

        entity.Property(e => e.FZIN)
            .HasMaxLength(45);

        entity.Property(e => e.FZOUT)
            .HasMaxLength(45);

        entity.Property(e => e.GoodsDescription)
            .IsRequired(true)   
            .HasMaxLength(255);

        entity.Property(e => e.GrossWeight).HasColumnName("grossWeight");

        entity.Property(e => e.NameOnPermit)
            .HasMaxLength(45);

        entity.Property(e => e.NotifyParty)
            .IsRequired(true)    
            .HasMaxLength(45);
        entity.Property(e => e.ShippingLine)
            .IsRequired(true)   
            .HasMaxLength(45);
        entity.Property(e => e.TypeOfMerchandise)
            .IsRequired(true)  
            .HasMaxLength(45);

        entity.Property(e => e.VoyageNumber)
            .IsRequired(true)   
            .HasMaxLength(45);

        entity.HasOne(d => d.Container)
            .WithMany(p => p.BillOfLoadings)
            .HasForeignKey(d => d.ContainerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Port)
            .WithMany(p => p.BillOfLoadings)
            .HasForeignKey(d => d.PortOfLoadingId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(b => b.BillOfLoadingDocument)
            .WithOne(d => d.BillOfLoading)
            .HasForeignKey<BillOfLoading>(b => b.BillOfLoadingDocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

          

        // entity.HasOne(d => d.Operation)
        //     .WithOne(p => p.BillOfLoading)
        //     .HasForeignKey<Operation>(d => d.BillOfLoadingId)
        //     .OnDelete(DeleteBehavior.ClientSetNull);

    }
}