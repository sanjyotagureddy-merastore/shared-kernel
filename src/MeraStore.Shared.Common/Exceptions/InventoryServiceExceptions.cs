using System.Net;
using MeraStore.Shared.Common.ErrorsCodes;

namespace MeraStore.Shared.Common.Exceptions;

public class InventoryServiceExceptions
{
  public class OutOfStockException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.InventoryService),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotFoundError),
    HttpStatusCode.NotFound, message);

  public class StockAdjustmentException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.InventoryService),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ServiceError),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.UnprocessableEntityError),
    HttpStatusCode.UnprocessableEntity, message);
}