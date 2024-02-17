using DevStore.Common.Messages.CommonMessages.Notifications;
using DevStore.Common.Messages;
using DevStore.Common.Communication.Mediator;
using DevStore.Sales.Domain;
namespace DevStore.Sales.Application.Commands;

public class BaseCommandHandler(
    IMediatorHandler mediatorHandler, 
    IOrderRepository orderRepository,
    ISalesUnitOfWork unitOfWork)
{
    protected readonly IMediatorHandler _mediatorHandler = mediatorHandler;
    protected readonly IOrderRepository _orderRepository = orderRepository;
    protected readonly ISalesUnitOfWork _unitOfWork = unitOfWork;

    protected bool ValidateCommand(Command message)
    {
        if (message.IsValid())
            return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        return false;
    }
}
