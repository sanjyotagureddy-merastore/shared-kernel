using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MeraStore.Shared.Common.Http.Extensions
{
  public static class HttpResponseExtensions
  {
    public static async Task<ApiResponse<TSuccess, ProblemDetails>> GetResponseOrError<TSuccess>(this HttpResponseMessage responseMessage)
        where TSuccess : class
    {
      var responseContent = await responseMessage.Content.ReadAsStringAsync();

      if (responseMessage.IsSuccessStatusCode)
      {
        var successResponse = JsonSerializer.Deserialize<TSuccess>(responseContent);
        return new ApiResponse<TSuccess, ProblemDetails>
        {
          Result = successResponse,
          Error = null,
          StatusCode = (int)responseMessage.StatusCode
        };
      }

      var errorResponse = JsonSerializer.Deserialize<ProblemDetails>(responseContent);
      return new ApiResponse<TSuccess, ProblemDetails>
      {
        Result = null,
        Error = errorResponse,
        StatusCode = (int)responseMessage.StatusCode
      };
    }

    // Overload for custom error type
    public static async Task<ApiResponse<TSuccess, TError>> GetResponseOrError<TSuccess, TError>(this HttpResponseMessage responseMessage)
        where TSuccess : class
        where TError : class
    {
      var responseContent = await responseMessage.Content.ReadAsStringAsync();

      if (responseMessage.IsSuccessStatusCode)
      {
        var successResponse = JsonSerializer.Deserialize<TSuccess>(responseContent);
        return new ApiResponse<TSuccess, TError>
        {
          Result = successResponse,
          Error = null,
          StatusCode = (int)responseMessage.StatusCode
        };
      }

      var errorResponse = JsonSerializer.Deserialize<TError>(responseContent);
      return new ApiResponse<TSuccess, TError>
      {
        Result = null,
        Error = errorResponse,
        StatusCode = (int)responseMessage.StatusCode
      };
    }
  }
}
