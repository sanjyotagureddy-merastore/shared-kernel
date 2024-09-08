namespace MeraStore.Shared.Common.Logging.Masking.Strategies
{
  public class EmailMaskingStrategy : IMaskingStrategy
  {
    public string Mask(string input)
    {
      if (string.IsNullOrEmpty(input) || !input.Contains("@")) return input;

      // Split the email into username and domain parts
      var parts = input.Split('@');
      if (parts.Length != 2) return input; // Invalid email format

      var username = parts[0];
      var domain = parts[1];

      // Mask the username while keeping the first and last characters visible
      var maskedUsername = username.Length > 2
        ? username[0] + new string('*', username.Length - 2) + username[^1]
        : new string('*', username.Length);

      // Return the masked username with the unmasked domain
      return maskedUsername + "@" + domain;
    }
  }
}