using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company> {
    public void Configure(EntityTypeBuilder<Company> entity) {
        
        entity.Property(e => e.Id).ValueGeneratedNever();
        
        entity.HasIndex(e => e.Id, "Id_UNIQUE")
            .IsUnique();

        entity.Property(e => e.CodeNIF)
            .HasMaxLength(45);
        
        entity.Property(e => e.Name)
            .HasMaxLength(45);

        entity.Property(e => e.TinNumber)
            .HasMaxLength(45);
        
        entity.HasOne(d => d.ContactPerson)
            .WithOne(p => p.Company)
            .HasForeignKey<Company>(d => d.ContactPersonId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        entity.HasOne(d => d.Address)
            .WithOne(p => p.Company)
            .HasForeignKey<Company>(d => d.AddressId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
    }

}