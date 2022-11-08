using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image> {
    
    public void Configure(EntityTypeBuilder<Image> entity) {

        entity.HasIndex(e => e.Id, "id_UNIQUE")
            .IsUnique();


    }
}