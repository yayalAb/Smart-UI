using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ECDDocumentConfiguration : IEntityTypeConfiguration<ECDDocument> {
    
    public void Configure(EntityTypeBuilder<ECDDocument> entity) {

        entity.HasOne(d => d.Operation)
            .WithMany(p => p.ECDDocuments)
            .HasForeignKey(d => d.OperationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_ECD Document_operation1");

    }
}