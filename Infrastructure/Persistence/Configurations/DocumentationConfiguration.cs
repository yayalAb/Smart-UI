using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DocumentationConfiguration : IEntityTypeConfiguration<Documentation> {
    
    public void Configure(EntityTypeBuilder<Documentation> entity) {
        entity.Property(e => e.Type)
            .IsRequired();

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
            
        entity.Property(e => e.BankPermit)
            .HasMaxLength(45);

        entity.Property(e => e.Date)
            .HasColumnType("datetime")
            .IsRequired();

        entity.Property(e => e.Destination)
            .HasMaxLength(45);


        entity.Property(e => e.InvoiceNumber)
            .HasMaxLength(45);


        entity.Property(e => e.Source)
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .HasMaxLength(45);

        entity.HasOne(d => d.Operation)
            .WithMany(p => p.Documentaions)
            .HasForeignKey(d => d.OperationId)
            .OnDelete(DeleteBehavior. Cascade);

    }
    
}