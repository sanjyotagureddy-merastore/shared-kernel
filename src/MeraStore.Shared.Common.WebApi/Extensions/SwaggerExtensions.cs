using MeraStore.Shared.Common.WebApi.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeraStore.Shared.Common.WebApi.Extensions;

public static class SwaggerExtensions
{
  // Overload that accepts a specific filter and version
  public static IServiceCollection AddCustomSwagger<TFilter>(
    this IServiceCollection services,
    string serviceName, string xmlFileName,
    string version = "v1") where TFilter : IOperationFilter
  {
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc(version, new OpenApiInfo { Title = serviceName, Version = version });

      // Add XML comments for better documentation
      var xmlFile = xmlFileName;
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      c.IncludeXmlComments(xmlPath);

      // Register the custom operation filter
      c.OperationFilter<TFilter>();
    });

    return services;
  }

  // Overload that uses the default filter and allows for versioning
  public static IServiceCollection AddCustomSwagger(
    this IServiceCollection services,
    string serviceName, string xmlFileName,
    string version = "v1")
  {
    return services.AddCustomSwagger<HeadersFilter>(serviceName, xmlFileName, version);
  }

  public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, string serviceName, string version = "v1")
  {
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{serviceName} {version}");
      c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });

    return app;
  }
}