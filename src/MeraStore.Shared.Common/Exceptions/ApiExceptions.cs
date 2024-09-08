using System.Net;
using MeraStore.Shared.Common.ErrorsCodes;

namespace MeraStore.Shared.Common.Exceptions;

public class ApiExceptions
{
  public class HttpRequestBuilderException(string message)
    : BaseAppException(ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway), EventCodeProvider.GetEventCode(Constants.EventCodes.HttpRequestError), 
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.HttpRequestError), HttpStatusCode.BadRequest, message);
  public class MissingUriException()
    : BaseAppException(ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway), EventCodeProvider.GetEventCode(Constants.EventCodes.HttpRequestError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.MissingUriError), HttpStatusCode.BadRequest,
      "URI must be set before building the request.");

  public class MissingMethodException()
    : BaseAppException(ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway), EventCodeProvider.GetEventCode(Constants.EventCodes.HttpRequestError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.MissingHttpMethodError), HttpStatusCode.BadRequest,
      "HTTP method must be set before building the request.");

  public class ServiceTimeoutException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway),
    EventCodeProvider.GetEventCode(Constants.EventCodes.Timeout),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.BadGatewayError),
    HttpStatusCode.GatewayTimeout, message);

  public class ServiceUnavailableException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceUnavailable),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ServiceUnavailableError),
    HttpStatusCode.ServiceUnavailable, message);

  public class DuplicateRecordException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceConflict),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConflictError),
    HttpStatusCode.Conflict, message);

  public class InvalidDataFormatException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.DataValidation),
    EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidParameter),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFormatError),
    HttpStatusCode.BadRequest, message);

  public class MissingConfigurationException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Configuration),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.MissingFieldError),
    HttpStatusCode.NotFound, message);

  public class InvalidEnvironmentConfigurationException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Configuration),
    EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidRequest),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFieldError),
    HttpStatusCode.BadRequest, message);

  public class ApiKeyMissingException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Security),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ApiKeyMissing),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.UnauthorizedError),
    HttpStatusCode.Unauthorized, message);

  public class TokenExpiredException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Security),
    EventCodeProvider.GetEventCode(Constants.EventCodes.TokenExpired),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.UnauthorizedError),
    HttpStatusCode.Unauthorized, message);

  public class FeatureNotSupportedException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Operational),
    EventCodeProvider.GetEventCode(Constants.EventCodes.FeatureNotSupported),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotImplementedError),
    HttpStatusCode.NotImplemented, message);

  public class ServiceLimitExceededException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Operational),
    EventCodeProvider.GetEventCode(Constants.EventCodes.RateLimitExceeded),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.TooManyRequestsError),
    HttpStatusCode.TooManyRequests, message);

  public class NetworkFailureException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Network),
    EventCodeProvider.GetEventCode(Constants.EventCodes.NetworkError),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.BadGatewayError),
    HttpStatusCode.BadGateway, message);

  public class ConnectionRefusedException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Network),
    EventCodeProvider.GetEventCode(Constants.EventCodes.NetworkError),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.BadGatewayError),
    HttpStatusCode.BadGateway, message);
}