using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ContainerConfiguration : IEntityTypeConfiguration<Container> {
    public void Configure(EntityTypeBuilder<Container> entity) {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
            
        entity.Property(e => e.ContianerNumber)
            .IsRequired()
            .HasMaxLength(45);
        entity.Property(e => e.Size)
            .IsRequired(true);
        entity.Property(e => e.Currency)
            .IsRequired(true);
        entity.Property(e => e.WeightMeasurement)
            .IsRequired(true);
        entity.Property(e => e.Article)
            .IsRequired(true);
        entity.Property(e => e.SealNumber)
            .IsRequired()
            .HasMaxLength(45);
        entity.HasOne(c => c.Operation)
            .WithMany(o => o.Containers)
            .HasForeignKey(c => c.OperationId)
            .OnDelete(DeleteBehavior.Cascade);
        entity.HasOne(c => c.LocationPort)
            .WithMany(p => p.Containers)
            .HasForeignKey(c => c.LocationPortId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasMany<TruckAssignment>(c => c.TruckAssignments)
            .WithMany(ta => ta.Containers);
        entity.HasOne(c => c.GeneratedDocument)
            .WithMany( d => d.Containers)
            .HasForeignKey(c => c.GeneratedDocumentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}