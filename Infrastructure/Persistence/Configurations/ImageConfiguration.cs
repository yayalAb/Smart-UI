using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image> {
    
    public void Configure(EntityTypeBuilder<Image> entity) {

        entity.HasIndex(e => e.Id, "id_UNIQUE")
            .IsUnique();

<<<<<<< HEAD
        entity.Property(e => e.Image1);
            // .HasColumnType("image");
=======
>>>>>>> 367bbcf7f2bd1c31da35a688daf67daee970e73a

    }
}