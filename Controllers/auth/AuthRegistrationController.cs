using KpiAlumni.Data;
using KpiAlumni.Models.ApiResponse;
using KpiAlumni.Models.Auth;
using KpiAlumni.Models.Common;
using KpiAlumni.Models.Firebase;
using KpiAlumni.Models.UserProvider;
using KpiAlumni.Tables;
using KpiAlumni.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Controllers.auth;

[ApiController]
[Route("api/v1/registration")]
public class AuthRegistrationController(AppDbContext _context) : Controller
{
    [HttpPost("with-email")]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> RegistrationAccountAsync(AuthRegProperty authRegProperty)
    {
        //--InitInfo
        var initId = HeadersInfo.GetInitId(HttpContext);

        //Check init id is valid
        var visitorInit = await InitializeOperation.ValidateInitIdAsync(_context, initId);

        if (visitorInit.Error != 0)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Invalid Init ID",
            });
        }

        //--Check Email id is already exist
        var emailExist = await _context.UserProvider.FirstOrDefaultAsync(x =>
            x.Identity == authRegProperty.Email && x.IdentityType == UserProviderIdentityTypes.EMAIL &&
            x.DeletedAt == 0);

        if (emailExist != null)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Email already exist",
            });
        }

        //--Create Local User Profile
        var providerFix = new UserPasswordProviderOperation();
        var fbProvider = await providerFix.ForceCreatePasswordProvider(HttpContext, _context, authRegProperty.Email,
            authRegProperty.Password1, authRegProperty.FullName);

        if (providerFix.userProvider == null)
        {
            return Ok(new ApiResponse
                {
                    Error = 1,
                    Message =providerFix.Message
                });
        }

        var primaryEmailProviderId = providerFix.userProvider.Id;
        var loginUserUid = fbProvider?.Uid;
        if (loginUserUid == null)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Technical Error",
            });
        }
        
        //--Create User Profile
        // Create New Profile
        var passHash = StringOperation.GenerateMd5(authRegProperty.Password1);
        var userProfile = await UserProfile.CreateProfile(HttpContext, _context, authRegProperty.FullName, primaryEmailProviderId, passHash);
        var userId = userProfile.Id;
        
        //--Update Local Provider 
        await providerFix.UpdateUserId(_context, userId);
        
        //-- TODO: Send Email Verification
        // var emailVerification = new EmailVerification();
        // await emailVerification.SendEmailVerificationAsync(HttpContext, _context, userId, primaryEmailProviderId, authRegProperty.Email);
        
        return Ok(new ApiResponse
        {
            Error = 0,
            Message = "Account Created Successfully",
        });
    }

    [HttpPost("with-provider")]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> RegistrationWithProviderAsync(
        AuthReg2Property regData)
    {
        //--InitInfo
        var initId = HeadersInfo.GetInitId(HttpContext);
        var reqUserUid = HeadersInfo.GetUserUid(HttpContext);
        
        //-- Check init id is  valid
        var initStatus = await InitializeOperation.ValidateInitIdAsync(_context, initId);
        if (initStatus.Error != 0)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Invalid Init ID",
            });
        }
        
        //--Validate Data
        var validateData = regData.ValidateAccountCreate2(reqUserUid);
        if (validateData.Error != 0)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = validateData.Message,
                Reference = validateData.Reference
            });
        }
        
        //--Collect Firebase User info
        var firebaseAuth = new FirebaseAuthService();
        var firebaseUser = await firebaseAuth.GetUserByUidAsync(reqUserUid);
        if(firebaseUser == null)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Error on Auth Data",
            });
        }

        var firebaseProviderData = firebaseUser.ProviderData.FirstOrDefault();
        var identity = firebaseProviderData?.Email;
        if (identity == null)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Error on Auth Data from Vendor",
            });
        }
        
        //--Create Local User profile (UID is same as Firebase UID)
        // Create Local User Profile (UID Requested)
        var localProvider = new UserProvider
        {
            UserId = 0,
            Provider = firebaseProviderData?.ProviderId ?? "",
            LocalProvider = firebaseUser.ProviderId,
            DisplayName = firebaseProviderData?.DisplayName ?? "",
            IdentityType = UserProviderIdentityTypes.EMAIL,
            Identity = identity,
            IsVerified = true,
            PhotoUrl = firebaseUser.PhotoUrl,
            ProviderUid = firebaseUser?.Uid ?? "",
            UserUid = reqUserUid,
            IsHide = false,
            Status = "active",
            IpString = IpOperation.GetIpString(HttpContext),
            CreatedAt = TimeOperation.GetUnixTime(),
            UpdatedAt = TimeOperation.GetUnixTime(),
            DeletedAt = 0,
        };
        _context.UserProvider.Add(localProvider);
        await _context.SaveChangesAsync();
        
        var primaryEmailProviderId = localProvider.Id;
        var passHash = "";
        var fullName = firebaseUser?.DisplayName ?? "";
        var photoUrl = firebaseUser?.PhotoUrl ?? "";
        var userProfile = await UserProfile.CreateProfile(HttpContext, _context, fullName,
            primaryEmailProviderId, passHash, photoUrl);
        var userId = userProfile.Id;
        
        //--Update Local Provider
        await _context.UserProvider.Where(x => x.Id == primaryEmailProviderId)
            .ForEachAsync(x => x.UserId = userId);
        await _context.SaveChangesAsync();
        
        return Ok(new ApiResponse
        {
            Error = 0,
            Message = "Account Created Successfully",
        });
    }
}
























