using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BankInformationConfiguration : IEntityTypeConfiguration<BankInformation> {

    public void Configure(EntityTypeBuilder<BankInformation> entity) {
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();


        entity.HasOne<Company>()
            .WithMany(c => c.BankInformation)
            .HasForeignKey(bi=>bi.CompanyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    
    }

}