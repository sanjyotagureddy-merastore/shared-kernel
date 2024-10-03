using MeraStore.Shared.Common.Core;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeraStore.Shared.Common.WebApi.Filters;

public class HeadersFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    // Check if the action method has the HeaderAttribute
    var customHeaderAttributes = context.MethodInfo.GetCustomAttributes(typeof(HeaderAttribute), true)
      .Cast<HeaderAttribute>();

    operation.Parameters ??= new List<OpenApiParameter>();

    if (!operation.Parameters.Any(p => p.Name == Constants.Headers.CorrelationId && p.In == ParameterLocation.Header))
    {
      operation.Parameters.Add(new OpenApiParameter
      {
        Name = Constants.Headers.CorrelationId,
        In = ParameterLocation.Header,
        Required = false, // Set to true if you want to make it mandatory
        Description = "Correlation ID for tracking requests across services.",
        Schema = new OpenApiSchema
        {
          Type = "string"
        }
      });
    }

    var headerAttributes = customHeaderAttributes.ToList();
    if (headerAttributes.Any())
    {
      operation.Parameters ??= new List<OpenApiParameter>();

      foreach (var headerAttribute in headerAttributes.Where(headerAttribute => !operation.Parameters.Any(p => p.Name == headerAttribute.Name && p.In == ParameterLocation.Header)))
      {
        operation.Parameters.Add(new OpenApiParameter
        {
          Name = headerAttribute.Name,
          In = ParameterLocation.Header,
          Required = headerAttribute.Required,
          Description = headerAttribute.Description ?? $"{headerAttribute.Name} header", // Use default description if none is provided
          Schema = new OpenApiSchema { Type = "string" }
        });
      }
    }
  }
}