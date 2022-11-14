using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver> {
    public void Configure(EntityTypeBuilder<Driver> entity) {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
            
        entity.Property(e => e.Fullname)
            .IsRequired()
            .HasMaxLength(45);

        entity.Property(e => e.LicenceNumber)
            .IsRequired()
            .HasMaxLength(45);

        entity.HasOne(d => d.Address)
            .WithOne(p => p.Driver)
            .HasForeignKey<Driver>(d => d.AddressId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
    }
}