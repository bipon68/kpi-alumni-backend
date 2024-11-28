namespace KpiAlumni.Models.Auth;

public class AuthLoginProperty
{
    public string InitId { get; set; } = "";

    public string IdentityType { get; set; } = "";
    
    public string Identity { get; set; } = "";

    public string Password { get; set; } = "";

    public string SentId { get; set; } = "";

    public string Otp { get; set; } = "";
}