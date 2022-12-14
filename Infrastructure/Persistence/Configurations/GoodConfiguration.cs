using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GoodConfiguration : IEntityTypeConfiguration<Good> {
    
    public void Configure(EntityTypeBuilder<Good> entity) {
        
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
            
        entity.Property(e => e.RemainingQuantity)
            .IsRequired(true);

        entity.Property(e => e.Description)
            .HasMaxLength(100);

        entity.Property(e => e.HSCode)
            .HasMaxLength(45);

        entity.Property(e => e.Manufacturer)
            .HasMaxLength(45);

        entity.Property(e => e.Unit)
            .IsRequired(false);

        entity.Property(e => e.UnitPrice)
            .IsRequired(false);
            
        entity.Property(e => e.CBM)
            .IsRequired(false);

        entity.HasOne(d => d.Container)
            .WithMany(p => p.Goods)
            .HasForeignKey(d => d.ContainerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.Operation)
            .WithMany(p => p.Goods)
            .HasForeignKey(d => d.OperationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(d => d.LocationPort)
            .WithMany(p => p.Goods)
            .HasForeignKey(d => d.LocationPortId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasMany(c => c.TruckAssignments)
            .WithMany(ta => ta.Goods);

    }
}