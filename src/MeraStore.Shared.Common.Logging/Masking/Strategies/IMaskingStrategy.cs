namespace MeraStore.Shared.Common.Logging.Masking.Strategies;

internal interface IMaskingStrategy
{
  string Mask(string input);
}
