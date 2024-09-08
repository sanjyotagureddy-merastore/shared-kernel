using System.Diagnostics.CodeAnalysis;

namespace MeraStore.Shared.Common.Logging.Attributes;

[ExcludeFromCodeCoverage]
public class EventCodeAttribute(string code): Attribute
{
    public string EventCode { get; } = code;
}
