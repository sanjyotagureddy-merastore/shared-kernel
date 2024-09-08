using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace MeraStore.Shared.Common.WebApi.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext context)
  {
    // Retrieve or generate a Correlation ID
    var correlationId = context.Request.Headers[Constants.Headers.CorrelationId].ToString();
    if (string.IsNullOrEmpty(correlationId))
    {
      correlationId = Guid.NewGuid().ToString();
      context.Request.Headers[Constants.Headers.CorrelationId] = correlationId;
    }

    // Push the Correlation ID to the Serilog log context
    using (LogContext.PushProperty("CorrelationId", correlationId))

      // Ensure the response headers collection is initialized and add the Correlation ID
      context.Response.OnStarting(() =>
      {
        if (!context.Response.Headers.ContainsKey(Constants.Headers.CorrelationId))
        {
          context.Response.Headers.Append(Constants.Headers.CorrelationId, correlationId);
        }
        return Task.CompletedTask;
      });

    // Continue processing the request
    await next(context);

  }
}