using MediatR;
using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Catalog.Application.Services;
using DevStore.Catalog.Data;
using DevStore.Catalog.Data.Repository;
using DevStore.Catalog.Domain;
using DevStore.Catalog.Domain.Events;
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
using DevStore.Payment.Data;
using DevStore.Payment.Data.Repository;
using DevStore.Payment.Business;
using DevStore.Payment.AntiCorruption;
using DevStore.Common.Messages.CommonMessages.IntegrationEvents;
using DevStore.Sales.Application.Commands.StartOrder;
using DevStore.Sales.Application.Commands.FinalizeOrder;
using DevStore.Sales.Application.Commands.CancelOrderProcessingAndReverseStock;
using DevStore.Sales.Application.Commands.CancelOrderProcessing;
using DevStore.Sales.Application.Events.OrderStockRejected;
using DevStore.Sales.Application.Events.PaymentMade;
using DevStore.Sales.Application.Events.PaymentRefused;
using DevStore.Payment.Business.Events;
using DevStore.Sales.Application.Events.VoucherApplied;
using DevStore.Sales.Application.Events.OrderFinalized;
using EventSourcing;
using DevStore.Common.Data.EventSourcing;

namespace DevStore.WebApp.VirtualStore.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Event Sourcing
        services.AddSingleton<IEventStoreService, EventStoreService>();
        services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

        // Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<CatalogContext>();

        services.AddScoped<INotificationHandler<LowStockProductEvent>, ProductEventHandler>();
        services.AddScoped<INotificationHandler<OrderStartedEvent>, ProductEventHandler>();
        services.AddScoped<INotificationHandler<OrderProcessingCanceledEvent>, ProductEventHandler>();

        // Sales
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderQueries, OrderQueries>();
        services.AddScoped<ISalesUnitOfWork, SalesUnitOfWork>();
        services.AddScoped<SalesContext>();

        services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, AddOrderItemHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, UpdateOrderItemHandler>();
        services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, RemoveOrderItemHandler>();
        services.AddScoped<IRequestHandler<ApplyOrderVoucherCommand, bool>, ApplyOrderVoucherHandler>();
        services.AddScoped<IRequestHandler<StartOrderCommand, bool>, StartOrderHandler>();
        services.AddScoped<IRequestHandler<FinalizeOrderCommand, bool>, FinalizeOrderHandler>();
        services.AddScoped<IRequestHandler<CancelOrderProcessingCommand, bool>, CancelOrderProcessingHandler>();
        services.AddScoped<IRequestHandler<CancelOrderProcessingAndReverseStockCommand, bool>, CancelOrderProcessingAndReverseStockHandler>();

        services.AddScoped<INotificationHandler<DraftOrderStartedEvent>, DraftOrderStartedHandler>();
        services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderItemAddedHandler>();
        services.AddScoped<INotificationHandler<OrderItemRemovedEvent>, OrderItemRemovedHandler>();
        services.AddScoped<INotificationHandler<OrderItemUpdatedEvent>, OrderItemUpdatedHandler>();
        services.AddScoped<INotificationHandler<OrderUpdatedEvent>, OrderUpdatedHandler>();
        services.AddScoped<INotificationHandler<OrderStockRejectedEvent>, OrderStockRejectedHandler>();
        services.AddScoped<INotificationHandler<PaymentMadeEvent>, PaymentMadeHandler>();
        services.AddScoped<INotificationHandler<PaymentRefusedEvent>, PaymentRefusedHandler>();
        services.AddScoped<INotificationHandler<VoucherAppliedEvent>, VoucherAppliedHandler>();
        services.AddScoped<INotificationHandler<OrderFinalizedEvent>, OrderFinalizedHandler>();

        // Payment
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentUnitOfWork, PaymentUnitOfWork>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICreditCardPaymentFacade, CreditCardPaymentFacade>();
        services.AddScoped<IPayPalGateway, PayPalGateway>();
        services.AddScoped<Payment.AntiCorruption.IConfigurationManager, Payment.AntiCorruption.ConfigurationManager>();
        services.AddScoped<PaymentContext>();

        services.AddScoped<INotificationHandler<OrderStockConfirmedEvent>, PaymentEventHandler>();

        return services;
    }
}
