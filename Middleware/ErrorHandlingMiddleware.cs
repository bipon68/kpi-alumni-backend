using KpiAlumni.Models.ApiResponse;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace KpiAlumni.Middleware;

public class ErrorHandlingMiddleware(
    RequestDelegate next,
    ILogger<ErrorHandlingMiddleware> logger,
    IWebHostEnvironment env)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        logger.LogError(ex, ex.Message);

        var code = StatusCodes.Status500InternalServerError;

        if (env.IsDevelopment())
        {
            JsonConvert.SerializeObject(new ApiResponse
            {
                Error = code,
                Message = ex.Message,
                Data = new
                {
                    StackTrace = ex.StackTrace
                }
            });
        }
        else
        {
            JsonConvert.SerializeObject(new ApiResponse
            {
                Error = code,
                Message = ex.Message,
                Data= new
                {
                    StackTrace = "An error occurred. Please try again later."
                }
            });
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(ApiResponseHandler.Error(ex.Message).ToJson());
    }
}