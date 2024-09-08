using System.Net;
using MeraStore.Shared.Common.ErrorsCodes;

namespace MeraStore.Shared.Common.Exceptions;

public class CommonExceptions
{
  public class ApiCommunicationException : BaseAppException
  {
    public ApiCommunicationException(string message) : base(ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.BadGatewayError),
      HttpStatusCode.BadGateway, message)
    {
    }
    public ApiCommunicationException(string message, Exception innerException) : base(ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ApiGateway),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.BadGatewayError),
      HttpStatusCode.BadGateway, message)
    {
    }
  }

  public class EventBusCommunicationException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.EventBus),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ServiceUnavailableError),
    HttpStatusCode.ServiceUnavailable, message);

  public class ValidationException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.DataValidation),
    EventCodeProvider.GetEventCode(Constants.EventCodes.DataValidationError),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ValidationError),
    HttpStatusCode.UnprocessableEntity, message);

  public class BusinessRuleException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.DataValidation),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceConflict),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.UnprocessableEntityError),
    HttpStatusCode.UnprocessableEntity, message);

  public class ConfigurationException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Configuration),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InternalServerError),
    HttpStatusCode.InternalServerError, message);

  public class DatabaseConnectionException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InternalServerError),
    HttpStatusCode.InternalServerError, message);

  public class DataNotFoundException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.DataNotFoundError),
    HttpStatusCode.NotFound, message);

  public class DataIntegrityException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
    EventCodeProvider.GetEventCode(Constants.EventCodes.DataIntegrityViolation),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConflictError),
    HttpStatusCode.Conflict, message);

  public class DuplicateDataException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.Database),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceConflict),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.ConflictError),
    HttpStatusCode.Conflict, message);
}