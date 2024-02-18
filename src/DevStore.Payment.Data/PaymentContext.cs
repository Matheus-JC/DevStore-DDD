using DevStore.Common.Messages;
using DevStore.Payment.Business;
using Microsoft.EntityFrameworkCore;

namespace DevStore.Payment.Data;

public class PaymentContext(DbContextOptions<PaymentContext> options) : DbContext(options)
{
    public DbSet<PaymentItem> Payments { get; set; }
    public DbSet<PaymentTransaction> Transactions { get; set; }

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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
