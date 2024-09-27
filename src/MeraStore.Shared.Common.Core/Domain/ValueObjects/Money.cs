using MeraStore.Shared.Common.Core.Enums;
using MeraStore.Shared.Common.Core.Exceptions;

namespace MeraStore.Shared.Common.Core.Domain.ValueObjects;

public class Money : IEquatable<Money>
{
  public decimal Amount { get; }
  public Currency Currency { get; }

  public Money(decimal amount, Currency currency = Currency.INR)
  {
    if (amount < 0) throw new CommonExceptions.InvalidDataFormatException("Amount cannot be negative");

    Amount = amount;
    Currency = currency;
  }

  public override bool Equals(object obj) => Equals(obj as Money);
  public bool Equals(Money other) => other != null &&
                                     Amount == other.Amount &&
                                     Currency == other.Currency;

  public override int GetHashCode() => HashCode.Combine(Amount, Currency);
}