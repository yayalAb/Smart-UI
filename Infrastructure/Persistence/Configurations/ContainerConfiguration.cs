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
            .IsRequired(); 
        entity.Property(e => e.Owner)
            .HasMaxLength(45);
        entity.HasOne(c => c.Operation)
            .WithMany(o => o.Containers)
            .HasForeignKey(c => c.OperationId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(c => c.LocationPort)
            .WithMany(p => p.Containers)
            .HasForeignKey(c => c.LocationPortId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        entity.HasOne(c => c.TruckAssignment)
            .WithMany(ta => ta.Containers)
            .HasForeignKey(c => c.TruckAssignmentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);




    }
}