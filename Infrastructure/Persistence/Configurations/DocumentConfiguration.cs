using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document> {
    
    public void Configure(EntityTypeBuilder<Document> entity) {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
            
        entity.HasOne(d => d.Operation)
            .WithOne(p => p.ECDDocument)
            .HasForeignKey<Operation>(d => d.ECDDocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_ECD Document_operation1");
        

    }
}