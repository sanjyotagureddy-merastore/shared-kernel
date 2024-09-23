namespace MeraStore.Shared.Common.Core;

public static class Constants
{
  public static readonly string ApplicationName = "ApplicationName";
  public static class Services
  {
    public static readonly string User = "merastore-user-service";
    public static readonly string Product = "merastore-product-service";
    public static readonly string Inventory = "merastore-inventory-service";
    public static readonly string Order = "merastore-order-service";
    public static readonly string Cart = "merastore-cart-service";
    public static readonly string Payment = "merastore-payment-service";
    public static readonly string Shipping = "merastore-shipping-service";
    public static readonly string Auth = "merastore-auth-service";
  }

  public static class SharedLibraries
  {
    public static readonly string SharedKernel = "merastore-shared-kernel";
    public static readonly string Crypto = "merastore-crypto-library";
    public static readonly string Serialization = "merastore-serialization-library";
    public static readonly string Logging = "merastore-logging-library";
    public static readonly string Caching = "merastore-caching-library";
    public static readonly string Configuration = "merastore-configuration-library";
    public static readonly string Validation = "merastore-validation-library";
    public static readonly string ErrorHandling = "merastore-error-handling-library";
    public static readonly string RateLimiting = "merastore-rate-limiting-library";
    public static readonly string HealthCheck = "merastore-health-check-library";
    public static readonly string Authorization = "merastore-authorization-library";
    public static readonly string Telemetry = "merastore-telemetry-library";
    public static readonly string I18n = "merastore-i18n-library";
  }

  public static class Infrastructure
  {
    public static readonly string Config = "merastore-infra-config";
    public static readonly string DockerImages = "merastore-docker-images";
    public static readonly string CiCdPipelines = "merastore-ci-cd-pipelines";
  }

  public static class Frontend
  {
    public static readonly string Web = "merastore-frontend";
    public static readonly string Mobile = "merastore-mobile-app";
  }

  public static class Additional
  {
    public static readonly string Docs = "merastore-docs";
    public static readonly string Monitoring = "merastore-monitoring";
    public static readonly string SecretsManagement = "merastore-secrets-management";
    public static readonly string Testing = "merastore-testing";
    public static readonly string Analytics = "merastore-analytics";
    public static readonly string FeatureFlags = "merastore-feature-flags";
  }

  public static class Headers
  {
    public static readonly string CorrelationId = "x-correlation-id";
    public static readonly string Accept = "Accept";
    public static readonly string Authorization = "Authorization";
    public static readonly string UserAgent = "User-Agent";
    public static readonly string CacheControl = "Cache-Control";
    public static readonly string Pragma = "Pragma";
    public static readonly string AcceptEncoding = "Accept-Encoding";
    public static readonly string AcceptLanguage = "Accept-Language";

    // Custom Application Headers
    public static readonly string RequestId = "x-request-id";
    public static readonly string UserId = "x-user-id";
    public static readonly string TenantId = "x-tenant-id";
    public static readonly string Role = "x-role";
    public static readonly string TraceId = "x-trace-id";
    public static readonly string SessionId = "x-session-id";
    public static readonly string Location = "x-location";
    public static readonly string DeviceId = "x-device-id";
    public static readonly string RetryAfter = "Retry-After";

    public static class ContentTypes
    {
      // Common Media Types
      public static readonly string Json = "application/json";
      public static readonly string Xml = "application/xml";
      public static readonly string Html = "text/html";
      public static readonly string PlainText = "text/plain";
      public static readonly string FormUrlEncoded = "application/x-www-form-urlencoded";
      public static readonly string MultipartFormData = "multipart/form-data";
    }
  }

  public static class SerilogIndex
  {

    public static readonly string ApplicationLog = "app-logs";
    public static readonly string AspNetCoreLogs = "aspnetcore-logs";
    public static readonly string RequestResponse = "request-response-logs";
  }

  public static class ServiceIdentifiers
  {
    public static readonly string General = "general";
    public static readonly string ApiGateway = "api-gateway";
    public static readonly string AuthService = "auth-service";
    public static readonly string CartService = "cart-service";
    public static readonly string Configuration = "configuration";
    public static readonly string Database = "database";
    public static readonly string DataValidation = "data-validation";
    public static readonly string EventBus = "event-bus";
    public static readonly string InventoryService = "inventory-service";
    public static readonly string Network = "network";
    public static readonly string Operational = "operational";
    public static readonly string OrderService = "order-service";
    public static readonly string ProductService = "product-service";
    public static readonly string Security = "security";
    public static readonly string UserService = "user-service";
  }

  public static class EventCodes
  {
    public const string ApiKeyMissing = "ApiKeyMissing";
    public const string DataIntegrityViolation = "DataIntegrityViolation";
    public const string DataValidationError = "DataValidationError";
    public const string FeatureNotSupported = "FeatureNotSupported";
    public const string Forbidden = "Forbidden";
    public const string InternalServerError = "InternalServerError";
    public const string InvalidParameter = "InvalidParameter";
    public const string InvalidRequest = "InvalidRequest";
    public const string MissingParameter = "MissingParameter";
    public const string NetworkError = "NetworkError";
    public const string NotImplemented = "NotImplemented";
    public const string RateLimitExceeded = "RateLimitExceeded";
    public const string RequestTimeout = "RequestTimeout";
    public const string ResourceConflict = "ResourceConflict";
    public const string ResourceNotFound = "ResourceNotFound";
    public const string ServiceError = "ServiceError";
    public const string ServiceUnavailable = "ServiceUnavailable";
    public const string Timeout = "Timeout";
    public const string TokenExpired = "TokenExpired";
    public const string UnauthorizedAccess = "UnauthorizedAccess";
    public const string UnsupportedMediaType = "UnsupportedMediaType";
    public const string DeserializationError = "DeserializationError";
    public const string InvalidOperation = "InvalidOperation";
    public const string OperationFailed = "OperationFailed";
    public const string SerializationError = "SerializationError";
    public const string HttpRequestError = "HttpRequestError";
  }

  public static class ErrorCodes
  {
    public const string BadGatewayError = "BadGatewayError";
    public const string BadRequestError = "BadRequestError";
    public const string ConflictError = "ConflictError";
    public const string DataNotFoundError = "DataNotFoundError";
    public const string ForbiddenError = "ForbiddenError";
    public const string InputError = "InputError";
    public const string InternalServerError = "InternalServerError";
    public const string InvalidFieldError = "InvalidFieldError";
    public const string InvalidFormatError = "InvalidFormatError";
    public const string MissingFieldError = "MissingFieldError";
    public const string NotFoundError = "NotFoundError";
    public const string NotImplementedError = "NotImplementedError";
    public const string RequestError = "RequestError";
    public const string ServiceUnavailableError = "ServiceUnavailableError";
    public const string TooManyRequestsError = "TooManyRequestsError";
    public const string UnauthorizedError = "UnauthorizedError";
    public const string UnprocessableEntityError = "UnprocessableEntityError";
    public const string ValidationError = "ValidationError";
    public const string HttpRequestError = "HttpRequestError";
    public const string MissingUriError = "MissingUriError";
    public const string MissingHttpMethodError = "MissingHttpMethodError";
  }

}