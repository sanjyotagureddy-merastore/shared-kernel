namespace MeraStore.Shared.Common.Logging.Masking.Strategies;

  public class CreditCardMaskingStrategy : IMaskingStrategy
  {
  public string Mask(string input)
  {
    if (string.IsNullOrEmpty(input)) return input;

    // Remove all non-numeric characters (like hyphens)
    var digitsOnly = new string(input.Where(char.IsDigit).ToArray());

    if (digitsOnly.Length < 4) return input;

    // Mask all but the last 4 digits
    var maskedDigits = new string('*', digitsOnly.Length - 4) + digitsOnly[^4..];

    // Reinsert the non-numeric characters (like hyphens)
    int digitIndex = 0;
    var maskedResult = input.Select(c => char.IsDigit(c) ? maskedDigits[digitIndex++] : c).ToArray();

    return new string(maskedResult);
  }
}
