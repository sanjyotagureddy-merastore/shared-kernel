using FluentAssertions;

using MeraStore.Shared.Common.Logging.Masking;
using MeraStore.Shared.Common.Logging.Models;

using Newtonsoft.Json;

namespace MeraStore.Shared.Common.Logging.Tests.Masking
{
  public class MaskingServiceTests
  {
    private string NormalizeJson(string json)
    {
      if (json == null) return null;

      try
      {
        return JsonConvert.SerializeObject(
          JsonConvert.DeserializeObject(json),
          Formatting.Indented
        );
      }
      catch (JsonException)
      {
        // Handle cases where the input is not valid JSON
        return json;
      }
    }

    [Fact]
    public void MaskSensitiveData_ShouldMaskSensitiveFieldsCorrectly()
    {
      // Arrange
      var maskingConfig = new Dictionary<MaskingType, List<string>>
            {
                { MaskingType.CreditCard, ["$.creditCardNumber"] },
                { MaskingType.Email, ["$.emailAddress"] },
                { MaskingType.PhoneNumber, ["$.phoneNumber"] },
                { MaskingType.Default, ["name"] }
            };

      var maskingService = new MaskingService(maskingConfig);

      var jsonContent = @"
            {
                ""creditCardNumber"": ""1234-5678-9876-5432"",
                ""emailAddress"": ""test@example.com"",
                ""phoneNumber"": ""123-456-7890"",
                ""nonSensitiveField"": ""Some Value"",
                ""name"": ""Some Value""
            }";

      // Act
      var result = maskingService.MaskSensitiveData(jsonContent);

      // Assert
      var expectedJson = @"
            {
                ""creditCardNumber"": ""****-****-****-5432"",
                ""emailAddress"": ""t**t@example.com"",
                ""phoneNumber"": ""***-***-7890"",
                ""nonSensitiveField"": ""Some Value"",
                ""name"": ""S**e V***e""
            }";

      NormalizeJson(result).Should().Be(NormalizeJson(expectedJson));
    }

    [Fact]
    public void MaskSensitiveData_ShouldReturnOriginalJson_WhenNoFieldsToMask()
    {
      // Arrange
      var maskingConfig = new Dictionary<MaskingType, List<string>>(); // No masking fields
      var maskingService = new MaskingService(maskingConfig);

      var jsonContent = @"
            {
                ""creditCardNumber"": ""1234-5678-9876-5432"",
                ""emailAddress"": ""test@example.com"",
                ""phoneNumber"": ""123-456-7890"",
                ""nonSensitiveField"": ""Some Value""
            }";

      // Act
      var result = maskingService.MaskSensitiveData(jsonContent);

      // Assert
      NormalizeJson(result).Should().Be(NormalizeJson(jsonContent));
    }

    [Fact]
    public void MaskSensitiveData_ShouldHandleEmptyOrNullJson()
    {
      // Arrange
      var maskingConfig = new Dictionary<MaskingType, List<string>>();
      var maskingService = new MaskingService(maskingConfig);

      // Act & Assert
      NormalizeJson(maskingService.MaskSensitiveData(null)).Should().Be(NormalizeJson(null));
      NormalizeJson(maskingService.MaskSensitiveData(string.Empty)).Should().Be(NormalizeJson(string.Empty));
    }

    [Fact]
    public void MaskSensitiveData_ShouldMaskUsingDefaultStrategy_WhenStrategyNotFound()
    {
      // Arrange
      var maskingConfig = new Dictionary<MaskingType, List<string>>
            {
                { MaskingType.Default, ["$.unknownField"] }
            };

      var maskingService = new MaskingService(maskingConfig);

      var jsonContent = @"
            {
                ""unknownField"": ""SomeSensitiveData"",
                ""nonSensitiveField"": ""Some Value""
            }";

      // Act
      var result = maskingService.MaskSensitiveData(jsonContent);

      // Assert
      var expectedJson = @"
            {
                ""unknownField"": ""S***************a"",
                ""nonSensitiveField"": ""Some Value""
            }"; // Expect no change since the strategy is not found

      NormalizeJson(result).Should().Be(NormalizeJson(expectedJson));
    }

    [Fact]
    public void MaskSensitiveData_ShouldMaskMultipleFields_Correctly()
    {
      // Arrange
      var maskingConfig = new Dictionary<MaskingType, List<string>>
            {
                { MaskingType.CreditCard, ["$.paymentDetails.creditCardNumber"] },
                { MaskingType.Email, ["$.user.emailAddress"] },
                { MaskingType.PhoneNumber, ["$.user.phoneNumber"] }
            };

      var maskingService = new MaskingService(maskingConfig);

      var jsonContent = @"
            {
                ""paymentDetails"": {
                    ""creditCardNumber"": ""1234-5678-9876-5432""
                },
                ""user"": {
                    ""emailAddress"": ""to@example.com"",
                    ""phoneNumber"": ""123-456-7890""
                },
                ""nonSensitiveField"": ""Some Value""
            }";

      // Act
      var result = maskingService.MaskSensitiveData(jsonContent);

      // Assert
      var expectedJson = @"
            {
                ""paymentDetails"": {
                    ""creditCardNumber"": ""****-****-****-5432""
                },
                ""user"": {
                    ""emailAddress"": ""**@example.com"",
                    ""phoneNumber"": ""***-***-7890""
                },
                ""nonSensitiveField"": ""Some Value""
            }";

      NormalizeJson(result).Should().Be(NormalizeJson(expectedJson));
    }
  }
}
