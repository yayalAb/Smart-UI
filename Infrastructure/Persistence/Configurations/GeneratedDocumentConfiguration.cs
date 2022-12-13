using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class GeneratedDocumentConfiguration : IEntityTypeConfiguration<GeneratedDocument>
{
    public void Configure(EntityTypeBuilder<GeneratedDocument> entity)
    {
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        entity.Property(e => e.LoadType)
            .IsRequired(true);
        entity.Property(e => e.DocumentType)
            .IsRequired(true);
        entity.HasOne(e => e.Operation)
            .WithMany(o => o.GeneratedDocuments)
            .HasForeignKey(e => e.OperationId);
        entity.HasOne(e => e.ExitPort)
            .WithMany(p => p.GeneratedDocuments)
            .HasForeignKey(e => e.ExitPortId);
        entity.HasOne(e => e.DestinationPort)
            .WithMany(p => p.GeneratedDocuments)
            .HasForeignKey(e => e.DestinationPortId);
        entity.HasOne(e => e.ContactPerson)
            .WithMany(c => c.GeneratedDocuments)
            .HasForeignKey(e => e.ContactPersonId);
    }
}