using KpiAlumni.Utils.Validation;

namespace KpiAlumni.Models.Auth;

public class AuthReg2Property
{
    public ApiResponse.ApiResponse ValidateAccountCreate2(string userUid)
    {
        var st = Validation.ValidateAll([
            Validation.IsTrue(userUid.Length > 5, "User UID is required"),
        ]);

        var refNames = new List<string>
        {
            "UserUid",
        };

        var refName = Validation.GetReferenceName(refNames, st.ErrorIndex);

        return new ApiResponse.ApiResponse { Error = st.Error, Message = st.Message, Reference = refName };
    }
}