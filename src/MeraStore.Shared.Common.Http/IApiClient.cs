namespace MeraStore.Shared.Common.Http;

public interface IApiClient
{
  Task<HttpResponseMessage> ExecuteAsync(HttpRequestBuilder requestBuilder);
}