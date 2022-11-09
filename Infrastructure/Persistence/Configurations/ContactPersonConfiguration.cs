using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ContactPersonConfiguration : IEntityTypeConfiguration<ContactPerson> {
    
    public void Configure(EntityTypeBuilder<ContactPerson> entity) {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Email)
            .HasMaxLength(45);

        entity.Property(e => e.Name)
            .HasMaxLength(45);

        entity.Property(e => e.Phone)
            .HasMaxLength(45);

    }
}