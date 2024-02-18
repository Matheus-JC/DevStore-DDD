using DevStore.Payment.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Payment.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<PaymentItem>
{
    public void Configure(EntityTypeBuilder<PaymentItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CardName)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(c => c.CardNumber)
            .IsRequired()
            .HasColumnType("varchar(16)");

        builder.Property(c => c.CardExpiration)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.Property(c => c.CardCvv)
            .IsRequired()
            .HasColumnType("varchar(4)");

        builder.HasOne(c => c.Transaction)
            .WithOne(c => c.Payment);

        builder.ToTable("Payments");
    }
}
