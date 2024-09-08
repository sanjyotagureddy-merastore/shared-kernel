using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using Serilog.Core;
using Serilog.Events;

namespace MeraStore.Shared.Common.Logging.Enrichers;

[ExcludeFromCodeCoverage]
public class IpAddressEnricher() : ILogEventEnricher
{
  private string _ipAddress;
  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (string.IsNullOrEmpty(_ipAddress))
    {
      _ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "";
    }
    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("IpAddress", _ipAddress));
  }
}
public class MethodEnricher(string method) : ILogEventEnricher
{
  private string _method;
  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (string.IsNullOrEmpty(_method))
    {
      _method = method ?? "";
    }
    logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Method", _method));
  }
}