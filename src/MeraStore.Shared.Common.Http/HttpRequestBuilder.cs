using MeraStore.Shared.Common.Core.Exceptions;
using Polly;

namespace MeraStore.Shared.Common.Http;

public class HttpRequestBuilder
{
  private readonly HttpRequestMessage _requestMessage = new();
  private readonly List<IAsyncPolicy> _policies = []; // Initialize with default policies
  private IAsyncPolicy? _timeoutPolicy = null;

  private static readonly List<IAsyncPolicy> DefaultPolicies =
  [
    Policy.Handle<HttpRequestException>()
      .WaitAndRetryAsync(new[]
      {
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(2),
        TimeSpan.FromSeconds(4)
      }),

    Policy.Handle<HttpRequestException>()
      .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30)),
  ];

  public HttpRequestBuilder WithMethod(HttpMethod method)
  {
    _requestMessage.Method = method;
    return this;
  }

  public HttpRequestBuilder WithUri(string uri)
  {
    _requestMessage.RequestUri = new Uri(uri);
    return this;
  }

  public HttpRequestBuilder WithHeader(string name, string value)
  {
    _requestMessage.Headers.Add(name, value);
    return this;
  }

  public HttpRequestBuilder WithContent(HttpContent content)
  {
    _requestMessage.Content = content;
    return this;
  }

  public HttpRequestBuilder AddPolicy(IAsyncPolicy policy)
  {
    _policies.Add(policy);
    return this;
  }
  
  public HttpRequestBuilder WithTimeout(TimeSpan timeout)
  {
    _timeoutPolicy = Policy.TimeoutAsync(timeout);
    _policies.Add(_timeoutPolicy);
    return this;
  }

  public HttpRequestMessage Build()
  {
    if (_requestMessage.RequestUri == null)
    {
      throw new ApiExceptions.MissingUriException();
    }

    if (_requestMessage.Method == null)
    {
      throw new ApiExceptions.MissingMethodException();
    }

    if (!_policies.Any())
    {
      _policies.AddRange(DefaultPolicies);
    }

    if(_timeoutPolicy == null)
    {
      _timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(60));
      _policies.Add(_timeoutPolicy);
    }

    return _requestMessage;
  }

  public IEnumerable<IAsyncPolicy> GetPolicies()
  {
    return _policies;
  }
}
