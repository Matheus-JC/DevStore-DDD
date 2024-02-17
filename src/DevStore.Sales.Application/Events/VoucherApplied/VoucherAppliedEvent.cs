using DevStore.Common.Messages;

namespace DevStore.Sales.Application.Events.VoucherApplied;

public class VoucherAppliedEvent : Event
{
    public Guid OrderId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid VoucherId { get; private set; }

    public VoucherAppliedEvent(Guid orderId, Guid clientId, Guid voucherId)
    {
        AggregateId = orderId;
        OrderId = orderId;
        ClientId = clientId;
        VoucherId = voucherId;
    }
}
