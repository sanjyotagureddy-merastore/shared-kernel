namespace MeraStore.Shared.Common.ErrorsCodes;

public class ErrorCodeProvider
{
  public static string GetErrorCode(string errorName)
  {
    if (ErrorCodes.Codes.TryGetValue(errorName, out var code))
    {
      return code;
    }
    throw new KeyNotFoundException($"Error code for '{errorName}' not found.");
  }

  public static string GetErrorKey(string errorCode)
  {
    foreach (var kvp in ErrorCodes.Codes.Where(kvp => kvp.Value == errorCode))
    {
      return kvp.Key;
    }

    throw new KeyNotFoundException($"Error key for code '{errorCode}' not found.");
  }
}