using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DocumentationConfiguration : IEntityTypeConfiguration<Documentation> {
    
    public void Configure(EntityTypeBuilder<Documentation> entity) {

        entity.Property(e => e.BankPermit)
            .HasMaxLength(45);

        entity.Property(e => e.City)
            .HasMaxLength(45);

        entity.Property(e => e.Country)
            .HasMaxLength(45);

        entity.Property(e => e.Date)
            .HasColumnType("datetime");

        entity.Property(e => e.Destination)
            .HasMaxLength(45);

        entity.Property(e => e.ImporterName)
            .HasMaxLength(45);

        entity.Property(e => e.InvoiceNumber)
            .HasMaxLength(45);

        entity.Property(e => e.Phone)
            .HasMaxLength(45);

        entity.Property(e => e.Source)
            .HasMaxLength(45);

        entity.Property(e => e.TinNumber)
            .HasMaxLength(45);

        entity.Property(e => e.TransportationMethod)
            .HasMaxLength(45);

        entity.Property(e => e.Type)
            .HasMaxLength(45);

        entity.HasOne(d => d.Operation)
            .WithOne(p => p.Documentaion)
            .HasForeignKey<Documentation>(d => d.OperationId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
    
}