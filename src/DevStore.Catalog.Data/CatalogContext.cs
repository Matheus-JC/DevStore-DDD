using DevStore.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevStore.Catalog.Data;

public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
