namespace MeraStore.Shared.Common.Core.Domain.ValueObjects;

public class Address(string street, string city, string state, string zipCode)
{
    public string Street { get; } = street;
    public string City { get; } = city;
    public string State { get; } = state;
    public string ZipCode { get; } = zipCode;
}