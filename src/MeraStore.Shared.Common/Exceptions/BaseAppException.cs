using System.Net;

namespace MeraStore.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class BaseAppException : Exception
{
  public string EventCode { get; }
  public string ServiceIdentifier { get; }
  public string ErrorCode { get; }
  public HttpStatusCode StatusCode { get; }
  public string FullErrorCode => $"{ServiceIdentifier}-{EventCode}-{ErrorCode}";
  public BaseAppException(string serviceIdentifier, string eventCode, string errorCode, HttpStatusCode statusCode, string message)
    : base(message)
  {
    ServiceIdentifier = serviceIdentifier;
    EventCode = eventCode;
    StatusCode = statusCode;
    ErrorCode = errorCode;
  }
  public BaseAppException(string serviceIdentifier, string eventCode, string errorCode, HttpStatusCode statusCode, Exception innerException)
    : base(innerException?.Message, innerException)
  {
    ServiceIdentifier = serviceIdentifier;
    EventCode = eventCode;
    StatusCode = statusCode;
    ErrorCode = errorCode;
  }
}