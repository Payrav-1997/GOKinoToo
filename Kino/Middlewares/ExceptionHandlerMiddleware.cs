using Kino.Common;
using Kino.Errors;
using Newtonsoft.Json;

namespace Kino.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(context, ex);
        }

    }

    private Task HandleExceptionMessageAsync(HttpContext context, Exception ex)
    {
        throw new Exception("Bruh");
    }
}
