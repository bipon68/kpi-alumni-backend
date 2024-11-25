using KpiAlumni.Models.ApiResponse;
using System.Text;
using NuGet.Protocol;

namespace KpiAlumni.Middleware;

public class NotFoundMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext hContext)
    {
        await next(hContext);
        if (hContext.Response is { StatusCode: 404, HasStarted: false })
        {
            hContext.Response.ContentType = "application/json";
            hContext.Response.StatusCode = 404;
            await hContext.Response.WriteAsync(ApiResponseHandler.Error("Not Found - 404").ToJson(), Encoding.UTF8);
        }
    }
}