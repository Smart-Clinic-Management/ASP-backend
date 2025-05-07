namespace SmartClinic.Application.Bases;
public class ResponseHandler
{

    public Response<T> Deleted<T>()
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Message = "Deleted Successfully"
        };
    }
    public Response<T> Success<T>(T entity, string? message = null, object? Meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Message = message ?? "Found Successfully",
        };
    }
    public Response<T> Unauthorized<T>()
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Message = "UnAuthorized"
        };
    }
    public Response<T> BadRequest<T>(string? Message = null, params List<string> errors)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Message = Message ?? "Bad Request",
            Errors = errors
        };
    }

    public Response<T> NotFound<T>(string? message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Message = message ?? "Not Found"
        };
    }

    public Response<T> Created<T>(T entity, object? Meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Message = "Created",
        };
    }

}