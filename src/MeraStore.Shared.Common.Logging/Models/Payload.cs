namespace MeraStore.Shared.Common.Logging.Models;

public class Payload
{
  public string Method { get; set; }
  public string Url { get; set; }
  public string Path { get; set; }
  public Dictionary<string, string> RequestHeaders { get; set; }
  public string Request { get; set; }
  public string RequestBodyUrl { get; set; }
  public string RequestBodyId { get; set; }
  public Dictionary<string, string> ResponseHeaders { get; set; }
  public string Response { get; set; }
  public string ResponseBodyUrl { get; set; }
  public string ResponseBodyId { get; set; }
  public int StatusCode { get; set; }
  public double TimeTakenSec { get; set; }
}
