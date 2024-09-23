namespace MeraStore.Shared.Common.Core.ErrorsCodes;

public static class ErrorCodes
{
  public static readonly Dictionary<string, string> Codes = new Dictionary<string, string>
    {
        { Constants.ErrorCodes.BadGatewayError, "502" },
        { Constants.ErrorCodes.BadRequestError, "400" },
        { Constants.ErrorCodes.ConflictError, "409" },
        { Constants.ErrorCodes.DataNotFoundError, "436" },
        { Constants.ErrorCodes.ForbiddenError, "403" },
        { Constants.ErrorCodes.InputError, "434" },
        { Constants.ErrorCodes.InternalServerError, "500" },
        { Constants.ErrorCodes.InvalidFieldError, "431" },
        { Constants.ErrorCodes.InvalidFormatError, "432" },
        { Constants.ErrorCodes.MissingFieldError, "430" },
        { Constants.ErrorCodes.NotFoundError, "404" },
        { Constants.ErrorCodes.NotImplementedError, "501" },
        { Constants.ErrorCodes.RequestError, "435" },
        { Constants.ErrorCodes.ServiceUnavailableError, "503" },
        { Constants.ErrorCodes.TooManyRequestsError, "429" },
        { Constants.ErrorCodes.UnauthorizedError, "401" },
        { Constants.ErrorCodes.UnprocessableEntityError, "422" },
        { Constants.ErrorCodes.ValidationError, "433" },
        { Constants.ErrorCodes.HttpRequestError, "437" },
        { Constants.ErrorCodes.MissingUriError, "438" },
        { Constants.ErrorCodes.MissingHttpMethodError, "439" }
    };
}
