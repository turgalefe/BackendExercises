using StudentsAPI.Model;
using System.Text.Json;
public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string message = "";
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }

        if (!context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";
            var response = new ExceptionResponse(message);
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}

// Extension method ile IApplicationBuilder altına custom methodumuzu eklenmesini sağlıyoruz.
public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorWrappingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorMiddleware>();
    }
}