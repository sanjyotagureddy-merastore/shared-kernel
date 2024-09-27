using System.Collections.Concurrent;

namespace MeraStore.Shared.Common.Core.Events;

public interface IEventPublisher
{
  Task PublishAsync<T>(T @event) where T : EventBase;
}
public class InMemoryEventPublisher : IEventPublisher
{
  private readonly ConcurrentDictionary<Type, Action<EventBase>> _handlers = new();

  public void Subscribe<T>(Action<T> handler) where T : EventBase
  {
    _handlers[typeof(T)] = e => handler((T)e);
  }

  public Task PublishAsync<T>(T @event) where T : EventBase
  {
    if (_handlers.TryGetValue(typeof(T), out var handler))
    {
      handler(@event);
    }
    return Task.CompletedTask;
  }
}