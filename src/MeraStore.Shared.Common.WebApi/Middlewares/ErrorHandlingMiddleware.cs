using System.Net;
using MeraStore.Shared.Common.Core;
using MeraStore.Shared.Common.Core.ErrorsCodes;
using MeraStore.Shared.Common.Logging.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using MeraStore.Shared.Common.Core.Exceptions;

namespace MeraStore.Shared.Common.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
  {
    private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore // Ignore null values
    };

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await next(context);
      }
      catch (FluentValidation.ValidationException ex)
      {
        await HandleValidationExceptionAsync(context, ex);
      }
      catch (BaseAppException ex)
      {
        await HandleExceptionAsync(context, ex);
      }
      catch (Exception ex)
      {
        var wrappedException = new BaseAppException(ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.General), GetRequestEventCode(context) ?? EventCodeProvider.GetEventCode(Constants.EventCodes.InternalServerError), ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InternalServerError), HttpStatusCode.InternalServerError, ex);

        // Handle unexpected exceptions with logging
        await HandleExceptionAsync(context, wrappedException);
      }
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException exception)
    {
      // Log the validation exception details immediately
      logger.LogError(exception, "Validation error occurred with details: {@ValidationErrors}",
          exception.Errors.GroupBy(x => x.PropertyName)
                  .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()));

      context.Response.ContentType = "application/problem+json";
      context.Response.StatusCode = 400; // Bad Request for validation errors

      var validationErrors = exception.Errors
          .GroupBy(x => x.PropertyName)
          .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

      var problemDetails = new ValidationProblemDetails(validationErrors)
      {
        Type = GetRequestEventCode(context),
        Status = context.Response.StatusCode,
        Title = "One or more validation errors occurred.",
        Instance = context.TraceIdentifier
      };
      LogContext.PushProperty("StatusCode", context.Response.StatusCode);
      await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, _jsonSerializerSettings));
    }

    private Task HandleExceptionAsync(HttpContext context, BaseAppException exception)
    {
      context.Response.ContentType = "application/problem+json";
      context.Response.StatusCode = (int)exception.StatusCode; // Default to 500 for unexpected errors

      var problemDetails = new ProblemDetails()
      {
        Status = context.Response.StatusCode,
        Type = GetRequestEventCode(context),
        Title = "An error occurred while processing your request.",
        Detail = exception.Message,
        Extensions =
                {
                    ["errorCode"] = exception.FullErrorCode,
                    ["service"] = ServiceProvider.GetServiceKey(exception.ServiceIdentifier)
                }
      };

      logger.LogError(exception, exception.Message);
      LogContext.PushProperty("StatusCode", context.Response.StatusCode);
      return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, _jsonSerializerSettings));
    }

    /// <summary>
    /// This gets the request event code associated with any endpoint of any service
    /// for quicker dereferencing and diagnosis.
    /// All endpoints to be created are to be decorated with `EventCodeAttribute`
    /// with a unique value.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private string GetRequestEventCode(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      var code = endpoint?.Metadata.GetMetadata<EventCodeAttribute>()?.EventCode ?? string.Empty;
      return code;
    }
  }
}
