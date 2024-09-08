namespace MeraStore.Shared.Common.Logging.Masking.Strategies;
public class DefaultMaskingStrategy : IMaskingStrategy
  {
      public string Mask(string input)
      {
          if (string.IsNullOrEmpty(input)) return input;

          var words = input.Split(' ');
          for (int i = 0; i < words.Length; i++)
          {
              words[i] = MaskWord(words[i]);
          }
          return string.Join(' ', words);
      }

      private string MaskWord(string word)
      {
          if (word.Length <= 2) return word;
          return word[0] + new string('*', word.Length - 2) + word[^1];
      }
  }
