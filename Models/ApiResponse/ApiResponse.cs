namespace KpiAlumni.Models.ApiResponse;

public class ApiResponse
{
    public int Error { get; set; }
    public string Message { get; set; } = "";
    public object Data { get; set; } = "";
    public string Reference { get; set; } = "";
}