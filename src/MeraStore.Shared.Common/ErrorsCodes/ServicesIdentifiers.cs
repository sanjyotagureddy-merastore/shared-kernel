namespace MeraStore.Shared.Common.ErrorsCodes;

public static class ServicesIdentifiers
{
  public static readonly Dictionary<string, string> ServiceCodes = new Dictionary<string, string>
  {
    { Constants.ServiceIdentifiers.ApiGateway, "07" },
    { Constants.ServiceIdentifiers.AuthService, "06" },
    { Constants.ServiceIdentifiers.CartService, "04" },
    { Constants.ServiceIdentifiers.Configuration, "11" },
    { Constants.ServiceIdentifiers.Database, "09" },
    { Constants.ServiceIdentifiers.DataValidation, "12" },
    { Constants.ServiceIdentifiers.EventBus, "08" },
    { Constants.ServiceIdentifiers.InventoryService, "03" },
    { Constants.ServiceIdentifiers.Network, "14" },
    { Constants.ServiceIdentifiers.Operational, "13" },
    { Constants.ServiceIdentifiers.OrderService, "05" },
    { Constants.ServiceIdentifiers.ProductService, "01" },
    { Constants.ServiceIdentifiers.Security, "10" },
    { Constants.ServiceIdentifiers.UserService, "02" },
    { Constants.ServiceIdentifiers.General, "15" }
  };
}