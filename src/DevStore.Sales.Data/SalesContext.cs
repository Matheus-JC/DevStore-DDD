using DevStore.Common.Messages;
using DevStore.Sales.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevStore.Sales.Data;

public class SalesContext(DbContextOptions<SalesContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entitiesTypes = modelBuilder.Model.GetEntityTypes();

        var properties = entitiesTypes.SelectMany(e =>
            e.GetProperties().Where(p =>
                p.ClrType == typeof(string)
            )
         );

        foreach (var property in properties)
            property.SetColumnType("varchar(100)");

        foreach (var relationship in entitiesTypes.SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

        modelBuilder.HasSequence<int>("ProductCodeSequence").StartsAt(1000).IncrementsBy(1);

        base.OnModelCreating(modelBuilder);
    }
}
