using DevStore.Common.Communication.Mediator;
using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Sales.Domain;
using MediatR;
using DevStore.Sales.Application.Events.VoucherApplied;
using DevStore.Sales.Application.Events.OrderUpdated;

namespace DevStore.Sales.Application.Commands.ApplyOrderVoucher;

public class ApplyOrderVoucherHandler(
    IMediatorHandler mediatorHandler,
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork
) : BaseCommandHandler(mediatorHandler, orderRepository, unitOfWork),
    IRequestHandler<ApplyOrderVoucherCommand, bool>
{
    public async Task<bool> Handle(ApplyOrderVoucherCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message))
            return false;

        var order = await _orderRepository.GetDraftOrderByClientId(message.ClientId);

        if (order is null)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found!"));
            return false;
        }
        var voucher = await _orderRepository.GetVoucherByCode(message.VoucherCode);

        if (voucher is null)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification("order", "Voucher not found!"));
            return false;
        }

        var result = order.ApplyVoucher(voucher);

        if (!result.IsValid)
        {
            foreach(var errorMessage in result.Messages)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("voucher", errorMessage));
            }

            return true;
        }

        order.AddEvent(new OrderUpdatedEvent(order.Id, message.ClientId, order.TotalValue));
        order.AddEvent(new VoucherAppliedEvent(order.Id, message.ClientId, voucher.Id));

        _orderRepository.UpdateVoucher(voucher);
        _orderRepository.Update(order);

        return await _unitOfWork.Commit();
    }
}
