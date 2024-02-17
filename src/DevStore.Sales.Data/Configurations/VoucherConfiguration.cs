using DevStore.Sales.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Sales.Data.Configurations;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Code)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.HasMany(v => v.Orders)
            .WithOne(o => o.Voucher)
            .HasForeignKey(o => o.VoucherId);

        builder.ToTable("Vouchers");
    }
}
