using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Services.Exceptions;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ValidationException)
        {
            httpContext.Response.StatusCode = 400;
            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync($"Validation exception occured: {exception.Message}", cancellationToken);

            return true;
        }
        else
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync($"Unhandled exception occured: {exception.Message}", cancellationToken);

            return true;
        }
    }
}