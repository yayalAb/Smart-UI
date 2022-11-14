

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OperationStatusConfigurations : IEntityTypeConfiguration<OperationStatus>
    {
        public void Configure(EntityTypeBuilder<OperationStatus> entity)
        {
            entity.Property(os => os.GeneratedDocumentName)
                .IsRequired();
            entity.Property(os => os.GeneratedDate)
                .IsRequired();
            entity.HasOne(os => os.Operation)
                .WithMany(o => o.OperationStatuses)
                .HasForeignKey(os => os.OperationId)
                .OnDelete(DeleteBehavior.ClientCascade);


        }
    }
}
