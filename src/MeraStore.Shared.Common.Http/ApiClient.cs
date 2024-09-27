using MeraStore.Shared.Common.Logging.Masking;
using MeraStore.Shared.Common.Logging.Models;
using Serilog;
using Serilog.Context;
using System.Diagnostics;
using MeraStore.Shared.Common.Logging.Enrichers;
using Nest;
using Policy = Polly.Policy;
using Microsoft.Extensions.Configuration;
using MeraStore.Shared.Common.Core.Exceptions;

namespace MeraStore.Shared.Common.Http
{
    public class ApiClient(ILogger logger, IElasticClient elasticClient, MaskingService maskingService, IConfiguration configuration)
    : IApiClient
  {
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IElasticClient _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
    private readonly MaskingService _maskingService = maskingService ?? throw new ArgumentNullException(nameof(maskingService));
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public async Task<HttpResponseMessage> ExecuteAsync(HttpRequestBuilder requestBuilder)
    {
      var request = requestBuilder.Build();
      var policies = requestBuilder.GetPolicies();

      using var client = new HttpClient();
      HttpResponseMessage response;
      var stopwatch = Stopwatch.StartNew();

      var requestBody = request.Content != null ? await request.Content.ReadAsStringAsync() : null;

      var payload = new Payload
      {
        Method = request.Method.ToString(),
        Url = request.RequestUri.ToString(),
        Path = request.RequestUri.AbsolutePath,
        RequestHeaders = request.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
      };

      try
      {
        var combinedPolicy = Policy.WrapAsync(policies.ToArray());

        // Mask request content
        var maskedRequestContent = requestBody != null ? _maskingService.MaskSensitiveData(requestBody) : null;
        payload.Request = maskedRequestContent;

        // Execute request
        response = await combinedPolicy.ExecuteAsync(() => client.SendAsync(request));

        // Capture and mask response content
        var responseContent = await response.Content.ReadAsStringAsync();
        var maskedResponseContent = _maskingService.MaskSensitiveData(responseContent);

        stopwatch.Stop();

        // Populate the rest of the payload
        payload.RequestBodyUrl = await IndexDocumentAndGetUrl(maskedRequestContent, "request");
        payload.ResponseHeaders = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value));
        payload.Response = maskedResponseContent;
        payload.ResponseBodyUrl = await IndexDocumentAndGetUrl(maskedResponseContent, "response");
        payload.StatusCode = (int)response.StatusCode;
        payload.TimeTakenSec = stopwatch.Elapsed.TotalSeconds;
      }
      catch
      {
        throw new CommonExceptions.ApiCommunicationException(
          $"An error occurred while executing the request: {request.Method} {request.RequestUri}");
      }

      // Log the payload
      await LogPayloadAsync(payload);

      return response;
    }

    private async Task<string> IndexDocumentAndGetUrl(string content, string documentType)
    {
      var document = new
      {
        Timestamp = DateTime.UtcNow,
        Type = documentType,
        Content = content
      };

      var response = await _elasticClient.IndexAsync(document, idx => idx.Index("request-response-logs"));

      if (!response.IsValid)
      {
        throw new Exception($"Failed to index document in Elasticsearch: {response.DebugInformation}");
      }

      var elasticUrl = _configuration["ElasticConfiguration:Uri"];
      // Construct the URL to view only the 'content' field of the document
      var documentUrl = $"{elasticUrl}/request-response-logs/_doc/{response.Id}?_source=content"; // Adjust the URL as needed

      return documentUrl;
    }

    private async Task LogPayloadAsync(Payload payload)
    {
      using (LogContext.Push(new MethodEnricher(payload.Method)))
      using (LogContext.PushProperty("Url", payload.Url))
      using (LogContext.PushProperty("Path", payload.Path))
      using (LogContext.PushProperty("RequestHeaders", payload.RequestHeaders))
      using (LogContext.PushProperty("Request", payload.Request))
      using (LogContext.PushProperty("RequestBodyUrl", payload.RequestBodyUrl))
      using (LogContext.PushProperty("ResponseHeaders", payload.ResponseHeaders))
      using (LogContext.PushProperty("Response", payload.Response))
      using (LogContext.PushProperty("ResponseBodyUrl", payload.ResponseBodyUrl))
      using (LogContext.PushProperty("StatusCode", payload.StatusCode))
      using (LogContext.PushProperty("TimeTakenSec", payload.TimeTakenSec))
      {
        await Task.Run(() => _logger.Information("API Request and Response Details {@payload}", payload));
      }
    }
  }
}