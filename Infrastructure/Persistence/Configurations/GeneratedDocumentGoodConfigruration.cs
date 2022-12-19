using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class GeneratedDocumentGoodConfiguration : IEntityTypeConfiguration<GeneratedDocumentGood>
{
    public void Configure(EntityTypeBuilder<GeneratedDocumentGood> entity)
    {
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Quantity)
            .IsRequired(true);

        entity.HasOne(e => e.Good)
            .WithMany(o => o.GeneratedDocumentsGoods)
            .HasForeignKey(e => e.GoodId);

        entity.HasOne(e => e.GeneratedDocument)
            .WithMany(p => p.GeneratedDocumentsGoods)
            .HasForeignKey(e => e.GeneratedDocumentId);

    }
}