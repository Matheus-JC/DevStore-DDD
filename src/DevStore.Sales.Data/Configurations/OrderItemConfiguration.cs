using DevStore.Sales.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Sales.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.ProductName)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.HasOne(o => o.Order)
            .WithMany(o => o.OrderItems);

        builder.ToTable("OrderItems");
    }
}
