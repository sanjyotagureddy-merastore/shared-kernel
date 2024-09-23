using System.Net;
using MeraStore.Shared.Common.Core.ErrorsCodes;

namespace MeraStore.Shared.Common.Core.Exceptions;

public class CartServiceExceptions
{
    public class CartNotFoundException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.CartService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotFoundError),
      HttpStatusCode.NotFound, message);

    public class CartItemNotFoundException(string message) : BaseAppException(
      ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.CartService),
      EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
      ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotFoundError),
      HttpStatusCode.NotFound, message);
}