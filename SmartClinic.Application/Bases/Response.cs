using System.Net;

namespace SmartClinic.Application.Bases;
public class Response<T>
{
    public Response()
    {

    }
    public Response(T? data, string? message = null)
    {
        Message = message;
        Data = data;
    }
    public Response(string message)
    {
        Message = message;
    }
    public Response(string message, bool succeeded)
    {
        Message = message;
    }

    public HttpStatusCode StatusCode { get; set; }

    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public T? Data { get; set; }
}