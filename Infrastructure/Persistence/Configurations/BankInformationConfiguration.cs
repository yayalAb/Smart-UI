using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BankInformationConfiguration : IEntityTypeConfiguration<BankInformation> {

    public void Configure(EntityTypeBuilder<BankInformation> entity) {
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        entity.Property(e => e.AccountHolderName)
            .IsRequired(true);
        entity.Property(e => e.BankName)
            .IsRequired(true);
        entity.Property(e => e.BankAddress)
            .IsRequired(true);
        entity.Property(e => e.SwiftCode)
            .IsRequired(true);
        entity.Property(e => e.CurrencyType)
            .IsRequired(true);
        
        entity.HasOne(bi => bi.Company)
            .WithMany(c => c.BankInformation)
            .HasForeignKey(bi=>bi.CompanyId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    
    }

}