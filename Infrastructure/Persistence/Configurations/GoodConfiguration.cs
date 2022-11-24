using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GoodConfiguration : IEntityTypeConfiguration<Good> {
    
    public void Configure(EntityTypeBuilder<Good> entity) {
        
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
            
        // entity.Property(e => e.CBM)
        //     .HasMaxLength(45);

        entity.Property(e => e.Description)
            
            .HasMaxLength(100);

        entity.Property(e => e.HSCode)
            .HasMaxLength(45);

        entity.Property(e => e.Manufacturer)
            .HasMaxLength(45);

        // entity.Property(e => e.UnitOfMeasurnment)
        //     .HasMaxLength(45);

        entity.HasOne(d => d.Container)
            .WithMany(p => p.Goods)
            .HasForeignKey(d => d.ContainerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(d => d.Operation)
            .WithMany(p => p.Goods)
            .HasForeignKey(d => d.OperationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);



    }
}