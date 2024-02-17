using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Domain;
using DevStore.Catalog.Domain.Events;
using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Application.Commands.AddOrderItem;
using DevStore.Sales.Application.Commands.ApplyOrderVoucher;
using DevStore.Sales.Application.Commands.RemoveOrderItem;
using DevStore.Sales.Application.Commands.UpdateOrderItem;
using DevStore.Sales.Application.Events.DraftOrderStarted;
using DevStore.Sales.Application.Events.OrderItemAdded;
using DevStore.Sales.Application.Events.OrderItemRemoved;
using DevStore.Sales.Application.Events.OrderItemUpdated;
using DevStore.Sales.Application.Events.OrderUpdated;
using DevStore.Sales.Application.Queries;
using DevStore.Sales.Data;
using DevStore.Sales.Data.Repository;
using DevStore.Sales.Domain;
using MediatR;

namespace DevStore.WebApp.VirtualStore.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();

        services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();

        // Sales
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderQueries, OrderQueries>();
        services.AddScoped<ISalesUnitOfWork, SalesUnitOfWork>();

        services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, AddOrderItemHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, UpdateOrderItemHandler>();
        services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, RemoveOrderItemHandler>();
        services.AddScoped<IRequestHandler<ApplyOrderVoucherCommand, bool>, ApplyOrderVoucherHandler>();

        services.AddScoped<INotificationHandler<DraftOrderStartedEvent>, DraftOrderStartedHandler>();
        services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderItemAddedHandler>();
        services.AddScoped<INotificationHandler<OrderItemRemovedEvent>, OrderItemRemovedHandler>();
        services.AddScoped<INotificationHandler<OrderItemUpdatedEvent>, OrderItemUpdatedHandler>();
        services.AddScoped<INotificationHandler<OrderUpdatedEvent>, OrderUpdatedHandler>();

        return services;
    }
}
