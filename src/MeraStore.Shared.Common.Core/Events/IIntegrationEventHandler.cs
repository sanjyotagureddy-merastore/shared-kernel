namespace MeraStore.Shared.Common.Core.Events;

public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
{
  Task Handle(TEvent integrationEvent);
}