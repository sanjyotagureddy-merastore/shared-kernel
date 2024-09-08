namespace MeraStore.Shared.Common.ErrorsCodes;

public static class EventCodes
{
  public static readonly Dictionary<string, string> Codes = new Dictionary<string, string>
  {
    { Constants.EventCodes.ApiKeyMissing, "111" },
    { Constants.EventCodes.DataIntegrityViolation, "116" },
    { Constants.EventCodes.DataValidationError, "107" },
    { Constants.EventCodes.FeatureNotSupported, "113" },
    { Constants.EventCodes.Forbidden, "104" },
    { Constants.EventCodes.InternalServerError, "106" },
    { Constants.EventCodes.InvalidParameter, "118" },
    { Constants.EventCodes.InvalidRequest, "102" },
    { Constants.EventCodes.MissingParameter, "117" },
    { Constants.EventCodes.NetworkError, "115" },
    { Constants.EventCodes.NotImplemented, "110" },
    { Constants.EventCodes.RateLimitExceeded, "114" },
    { Constants.EventCodes.RequestTimeout, "119" },
    { Constants.EventCodes.ResourceConflict, "105" },
    { Constants.EventCodes.ResourceNotFound, "101" },
    { Constants.EventCodes.ServiceError, "109" },
    { Constants.EventCodes.ServiceUnavailable, "100" },
    { Constants.EventCodes.Timeout, "108" },
    { Constants.EventCodes.TokenExpired, "112" },
    { Constants.EventCodes.UnauthorizedAccess, "103" },
    { Constants.EventCodes.UnsupportedMediaType, "120" },
    { Constants.EventCodes.DeserializationError, "121" },
    { Constants.EventCodes.SerializationError, "122" },
    { Constants.EventCodes.OperationFailed, "124" },
    { Constants.EventCodes.InvalidOperation, "123" }
  };
}