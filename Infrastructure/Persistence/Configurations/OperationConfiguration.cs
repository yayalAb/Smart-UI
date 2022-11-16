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
            .HasForeignKey(d => d.ShippingAgentId);

    }
}