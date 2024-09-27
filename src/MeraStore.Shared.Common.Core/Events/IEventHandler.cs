namespace MeraStore.Shared.Common.Core.Events;

public interface IEventHandler<TEvent> where TEvent : EventBase
{
  Task Handle(TEvent domainEvent);
}