

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TruckAssignmentConfiguarations : IEntityTypeConfiguration<TruckAssignment>
    {
        public void Configure(EntityTypeBuilder<TruckAssignment> entity)
        {
            entity.HasOne(d => d.Driver)
                .WithMany(p => p.TruckAssignments)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
            entity.HasOne(d => d.Truck)
                .WithMany(p => p.TruckAssignments)
                .HasForeignKey(d => d.TruckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
            entity.HasOne(d => d.Operation)
                .WithMany(p => p.TruckAssignments)
                .HasForeignKey(d => d.OperationId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.SourcePort)
                .WithMany(p => p.SourcePortTruckAssignments)
                .HasForeignKey(d => d.SourcePortId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
            entity.HasOne(d => d.DestinationPort)
                .WithMany(p => p.DestinationPortTruckAssignments)
                .HasForeignKey(d => d.DestinationPortId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
        }
    }
}
