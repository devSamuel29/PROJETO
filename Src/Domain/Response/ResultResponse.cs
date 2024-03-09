namespace Src.Domain.Response;

public class ResultResponse
{
    public int StatusCode { get; set; }

    public dynamic? Data { get; set; }
}
