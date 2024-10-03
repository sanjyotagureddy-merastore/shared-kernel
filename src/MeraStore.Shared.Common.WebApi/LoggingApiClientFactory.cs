using MeraStore.Service.Logging.SDK;

namespace MeraStore.Shared.Common.WebApi;
public class LoggingApiClientFactory
{
  private static volatile LoggingApiClient _clientInstance; // Mark as volatile

  private static readonly object Lock = new object();

  private LoggingApiClientFactory() { }

  public static LoggingApiClient GetClient()
  {
    // First check without locking (fast path)
    if (_clientInstance != null) return _clientInstance;

    // Lock to create a new instance if it doesn't exist
    lock (Lock)
    {
      // Second check inside the lock (slow path)
      if (_clientInstance == null) // Double-check locking
      {
        var baseUrl = "http://logging-api.merastore.com:8101";
        var builder = new ClientBuilder(); // Make sure the correct builder is used
        _clientInstance = builder
          .WithUrl(baseUrl)
          .UseDefaultRetryPolicy()
          .Build();
      }
    }

    return _clientInstance;
  }
}
