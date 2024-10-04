using MeraStore.Shared.Common.Logging.Masking.Strategies;
using MeraStore.Shared.Common.Logging.Models;

using Newtonsoft.Json.Linq;

namespace MeraStore.Shared.Common.Logging.Masking;

public class MaskingService(Dictionary<MaskingType, List<string>> fieldStrategyMap)
{
  private readonly Dictionary<MaskingType, IMaskingStrategy> _strategies = new()
    {
        { MaskingType.CreditCard, new CreditCardMaskingStrategy() },
        { MaskingType.Email, new EmailMaskingStrategy() },
        { MaskingType.PhoneNumber, new PhoneNumberMaskingStrategy() },
        { MaskingType.Default, new DefaultMaskingStrategy() }
    };

  public string MaskSensitiveData(string jsonContent)
  {
    if (string.IsNullOrEmpty(jsonContent)) return jsonContent;

    var jsonObject = JToken.Parse(jsonContent); // Parse as JToken to handle both arrays and objects

    MaskSensitiveData(jsonObject);

    return jsonObject.ToString();
  }

  private void MaskSensitiveData(JToken jsonToken)
  {
    // If the token is an array, iterate through its items
    if (jsonToken.Type == JTokenType.Array)
    {
      foreach (var item in jsonToken.Children())
      {
        MaskSensitiveData(item); // Recursively mask each item
      }
    }
    else if (jsonToken.Type == JTokenType.Object) // If the token is an object
    {
      foreach (var strategyEntry in fieldStrategyMap)
      {
        var strategyName = strategyEntry.Key;
        var fields = strategyEntry.Value;

        if (!_strategies.TryGetValue(strategyName, out var strategy)) continue;

        foreach (var fieldPath in fields)
        {
          var tokens = jsonToken.SelectTokens(fieldPath, errorWhenNoMatch: false).ToList();
          foreach (var token in tokens)
          {
            if (token.Type != JTokenType.String) continue;

            var originalValue = token.ToString();
            var maskedValue = strategy.Mask(originalValue);
            token.Replace(maskedValue);
          }
        }
      }
    }
  }
}
