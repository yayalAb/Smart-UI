using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company> {
    public void Configure(EntityTypeBuilder<Company> entity) {
        
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
            
        entity.Property(e => e.CodeNIF)
            .HasMaxLength(45);
        
        entity.Property(e => e.Name)
            .HasMaxLength(45);

        entity.Property(e => e.TinNumber)
            .HasMaxLength(45);
        
        entity.HasOne(d => d.Address)
            .WithOne(p => p.Company)
            .HasForeignKey<Company>(d => d.AddressId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior. ClientSetNull);

        entity.HasMany(c => c.DefaultSetting)
            .WithOne(s => s.DefaultCompany)
            .HasForeignKey(s => s.CompanyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior. ClientSetNull);
    }

}