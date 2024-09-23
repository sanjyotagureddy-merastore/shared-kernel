namespace MeraStore.Shared.Common.Core.ErrorsCodes;

public class EventCodeProvider
{
  public static string GetEventCode(string eventName)
  {
    if (EventCodes.Codes.TryGetValue(eventName, out var code))
    {
      return code;
    }
    throw new KeyNotFoundException($"Event code for '{eventName}' not found.");
  }

  public static string GetEventKey(string eventCode)
  {
    foreach (var kvp in EventCodes.Codes)
    {
      if (kvp.Value == eventCode)
      {
        return kvp.Key;
      }
    }
    throw new KeyNotFoundException($"Event key for code '{eventCode}' not found.");
  }
}