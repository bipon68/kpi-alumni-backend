using System.ComponentModel.DataAnnotations;
using KpiAlumni.Models.ApiResponse;
using Newtonsoft.Json;

namespace KpiAlumni.Middleware;

public class ValidationErrorHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (context.Response.StatusCode != StatusCodes.Status400BadRequest)
            {
                await _next(context);
            }
            else
            {
                var ex = new ValidationException("Validation Error");
                await HandleExceptionAsync(context, ex);
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Check if the error is a validation error
        if (context.Response.StatusCode == 400 && exception is ValidationException)
        {
            var result = new ApiResponse
            {
                Error = 1,
                Message = exception.Message,
                Data = exception.Data
            };

            // Handle validation errors
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
        else
        {
            var result = new ApiResponse
            {
                Error = 1,
                Message = "An unexpected error occurred",
                Data = exception.Data
            };

            // Handle other types of exceptions
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}