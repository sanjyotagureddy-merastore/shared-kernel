using MediatR;

namespace MeraStore.Shared.Common.Core.Events;

public abstract class EventBase : INotification
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
}
public abstract class DomainEvent : EventBase
{
}