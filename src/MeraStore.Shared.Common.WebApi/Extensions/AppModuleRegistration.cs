using System.Diagnostics.CodeAnalysis;
using MeraStore.Shared.Common.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MeraStore.Shared.Common.WebApi.Extensions;

[ExcludeFromCodeCoverage]
public static class AppModuleRegistration
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        return app;
    }
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApiLoggingMiddleware>();
        return app;
    }
}