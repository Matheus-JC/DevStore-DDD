using MediatR;

namespace DevStore.Sales.Application.Events.VoucherApplied;

public class VoucherAppliedHandler : INotificationHandler<VoucherAppliedEvent>
{
    public Task Handle(VoucherAppliedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
