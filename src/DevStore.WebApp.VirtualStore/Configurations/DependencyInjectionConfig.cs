using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Data.UnitOfWork;
using DevStore.Catalog.Domain;
using DevStore.Common.Communication;
using DevStore.Common.Data;

namespace DevStore.WebApp.VirtualStore.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();

        return services;
    }
}
