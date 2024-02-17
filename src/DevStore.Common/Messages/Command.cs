using FluentValidation.Results;
using MediatR;

namespace DevStore.Common.Messages;

public abstract class Command : Message, IRequest<bool>
{
    public DateTime Timestamp { get; private set; } = DateTime.Now;
    public ValidationResult ValidationResult { get; set; }

    public abstract bool IsValid();
}
