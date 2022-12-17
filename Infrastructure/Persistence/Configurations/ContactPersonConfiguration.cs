using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ContactPersonConfiguration : IEntityTypeConfiguration<ContactPerson>
{

    public void Configure(EntityTypeBuilder<ContactPerson> entity)
    {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Email)
            .HasMaxLength(45);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(45);

        entity.Property(e => e.Phone)
            .HasMaxLength(45);
        entity.HasOne(e => e.Company)
            .WithMany(c => c.ContactPeople)
            .HasForeignKey(e => e.CompanyId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
        entity.HasOne(e => e.Operation)
            .WithOne(o => o.ContactPerson)
            .HasForeignKey<Operation>(o => o.ContactPersonId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}