namespace MeraStore.Shared.Common.Core.Events;

public interface IEventHandler<TEvent> where TEvent : DomainEventBase
{
  Task Handle(TEvent domainEvent);
}