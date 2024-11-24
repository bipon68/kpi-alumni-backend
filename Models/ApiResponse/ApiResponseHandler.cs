namespace KpiAlumni.Models.ApiResponse;

public class ApiResponseHandler
{
    public static ApiResponse Success(string message)
    {
        return new ApiResponse
        {
            Error = 0,
            Message = message,
            Data = new { },
            Reference = ""
        };
    }
    
    public static ApiResponse Success(string message, object data)
    {
        return new ApiResponse
        {
            Error = 0,
            Message = message,
            Data = data,
            Reference = ""
        };
    }
    
    public static ApiResponse Success(string message, string data)
    {
        return new ApiResponse
        {
            Error = 0,
            Message = message,
            Data = data,
            Reference = ""
        };
    }
    
    public static ApiResponse Error(string message = "Error", string ReferenceName = "")
    {
        return new ApiResponse
        {
            Error = 1,
            Message = message,
            Data = new { },
            Reference = ReferenceName,
        };
    }
    
    public static ApiResponse Error(ApiResponse apiResponse)
    {
        return new ApiResponse
        {
            Error = 1,
            Message = apiResponse.Message,
            Data = new { },
            Reference = apiResponse.Reference
        };
    }
    
    public static ApiResponse NotFound(string message)
    {
        return new ApiResponse
        {
            Error = 1,
            Message = message,
            Data = new { },
            Reference = "",
        };
    }
    
    public static ApiResponse BadRequest(Exception ex)
    {
        return new ApiResponse
        {
            Error = 1,
            Message = ex.Message,
            Data = new { },
            Reference = "",
        };
    }
}