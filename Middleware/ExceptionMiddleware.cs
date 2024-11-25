using System.Net;
using KpiAlumni.Models.ApiResponse;
using NuGet.Protocol;

namespace KpiAlumni.Middleware;

public class ExceptionMiddleware(RequestDelegate _next, ILogger<ExceptionMiddleware> _logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(ApiResponseHandler.Error(ex.Message).ToJson());
    }
}