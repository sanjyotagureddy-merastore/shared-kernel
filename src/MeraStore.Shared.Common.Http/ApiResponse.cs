namespace MeraStore.Shared.Common.Http;

public class ApiResponse<TResponse, TError>
{
  public TResponse? Result { get; set; }
  public TError? Error { get; set; }
  public int StatusCode { get; set; }
  public bool IsSuccess => Error == null;
}
