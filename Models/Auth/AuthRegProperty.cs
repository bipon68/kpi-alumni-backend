using KpiAlumni.Utils.Validation;

namespace KpiAlumni.Models.Auth;

public class AuthRegProperty
{
    
    public string FullName { get; set; } = "";
    
    public string Email { get; set; } = "";
    public string Password1 { get; set; } = "";
    public string Password2 { get; set; } = "";
    public bool AcceptTc { get; set; } = false;
    
    public ApiResponse.ApiResponse ValidateAccountCreate()
    {
        var st = Validation .ValidateAll([
            //Validation.IsValidString(InitId, "Init ID", 32, 32),
            Validation.IsValidString(FullName, "First Name", 1),
            Validation.IsValidEmailFormat(Email, "Email"),
            Validation.IsValidPasswordFormat(Password1, "Password"),
            Validation.IsTrue(Password1 == Password2, "Password must be same"),
            Validation.IsTrue(AcceptTc, "User must Accept Terms & Conditions"),
        ]);

        var refNames = new List<string>
        {
            "FullName",
            "Email",
            "Password1",
            "Password2",
            "AcceptTc",
        };

        var referenceName = Validation.GetReferenceName(refNames, st.ErrorIndex);

        return new ApiResponse.ApiResponse { Error = st.Error, Message = st.Message, Reference = referenceName };
    }
}