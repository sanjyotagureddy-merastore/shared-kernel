namespace MeraStore.Shared.Common.ErrorsCodes;

public class ServiceProvider
{
  public static string GetServiceCode(string serviceName)
  {
    if (ServicesIdentifiers.ServiceCodes.TryGetValue(serviceName, out var code))
    {
      return code;
    }
    throw new KeyNotFoundException($"Service code for '{serviceName}' not found.");
  }

  public static string GetServiceKey(string serviceCode)
  {
    foreach (var kvp in ServicesIdentifiers.ServiceCodes)
    {
      if (kvp.Value == serviceCode)
      {
        return kvp.Key;
      }
    }
    throw new KeyNotFoundException($"Service key for code '{serviceCode}' not found.");
  }
}