using MeraStore.Shared.Common.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace MeraStore.Shared.Common.WebApi;

[ExcludeFromCodeCoverage]
public static class AppExtensions
{
  public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
  {
    app.UseMiddleware<ErrorHandlingMiddleware>();
    return app;
  }

  public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
  {
    app.UseMiddleware<CorrelationIdMiddleware>();
    return app;
  }
  public static IApplicationBuilder UseApiLogging(this IApplicationBuilder app)
  {
    app.UseMiddleware<RequestResponseLoggingMiddleware>();
    return app;
  }

  public static IApplicationBuilder UseCommonMiddlewares(this IApplicationBuilder app)
  {
    app.UseCustomExceptionHandler();
    app.UseCorrelationId();
    app.UseApiLogging();
    app.UseCustomExceptionHandler();
    return app;
  }
}