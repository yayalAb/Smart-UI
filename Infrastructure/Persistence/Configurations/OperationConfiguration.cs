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
        entity.HasIndex(e => e.OperationNumber)
               .IsUnique();

        entity.Property(e => e.BillNumber)
            .IsRequired(true)
            .HasMaxLength(45);

        entity.Property(e => e.Quantity)
            .IsRequired(false)
            .HasMaxLength(45);

        entity.Property(e => e.GrossWeight)
            .IsRequired(true)
            .HasMaxLength(45);

        entity.Property(e => e.DestinationType)
            .IsRequired(true)
            .HasMaxLength(45);

        entity.Property(e => e.Status)
            .IsRequired(true) 
            .HasMaxLength(45);
//---------------------------------------------------------------------
        entity.Property(e => e.SNumber)
            .IsRequired(false);   

        entity.Property(e => e.SDate)
            .IsRequired(false)   
            .HasColumnType("datetime");

        entity.Property(e => e.RecepientName)
            .IsRequired(false); 

        entity.Property(e => e.VesselName)
            .IsRequired(false); 

        entity.Property(e => e.ArrivalDate)
            .IsRequired(false)   
            .HasColumnType("datetime");
        entity.Property(e => e.ConnaissementNumber)
            .IsRequired(false);
        entity.Property(e => e.CountryOfOrigin)
            .IsRequired(false);
        entity.Property(e => e.REGTax)
            .IsRequired(false);

//--------------------------------------------------------------------------------------
        entity.HasOne(d => d.Company)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.CompanyId);

        entity.HasOne(d => d.PortOfLoading)
            .WithMany(p => p.Operations)
            .HasForeignKey(d => d.PortOfLoadingId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);


        entity.HasOne(d => d.ShippingAgent)
            .WithMany(p => p.Operations)
            .IsRequired(false)
            .HasForeignKey(d => d.ShippingAgentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}