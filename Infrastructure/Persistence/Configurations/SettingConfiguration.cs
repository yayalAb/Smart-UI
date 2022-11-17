using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting> {

    public void Configure(EntityTypeBuilder<Setting> entity) {

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Email)
            .IsRequired(true)
            .HasMaxLength(60);

        entity.Property(e => e.Password)
            .IsRequired(true)
            .HasMaxLength(100);

        entity.Property(e => e.Username)
            .HasMaxLength(45)
            .IsRequired(true);

        entity.Property(e => e.Host)
            .HasMaxLength(50)
            .IsRequired(true);

        entity.Property(e => e.Port)
            .HasMaxLength(10)
            .IsRequired(true);

        entity.Property(e => e.Protocol)
            .HasMaxLength(10)
            .IsRequired(true);
        
    }

}