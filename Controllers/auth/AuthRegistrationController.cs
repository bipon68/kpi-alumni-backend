using KpiAlumni.Data;
using KpiAlumni.Models.ApiResponse;
using KpiAlumni.Models.Auth;
using KpiAlumni.Models.Common;
using KpiAlumni.Models.Firebase;
using KpiAlumni.Models.Initialize;
using KpiAlumni.Models.ListStatus;
using KpiAlumni.Models.UserProfile;
using KpiAlumni.Models.UserProvider;
using KpiAlumni.Tables;
using KpiAlumni.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Controllers.auth;

[ApiController]
[Route("api/v1/registration")]
public class AuthRegistrationController(AppDbContext _context) : Controller
{
    [HttpPost("with-email")]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> RegistrationAsync()
    {
        //-- Validate the request
        //-- Check if the user already exists
        //-- Create a new user
        //-- Send a verification email
        //-- Return a response
        return Ok(new ApiResponse
        {
            Error = 0,
            Message = "Registration Info"
        });
    }
    
    [HttpPost("with-provider")]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> RegistrationWithProviderAsync(AuthRegProviderProperty data)
    {

        var timeNow = TimeOperation.GetUnixTime();
        var initId = HeadersInfo.GetInitId(HttpContext);
        var userUid = HeadersInfo.GetUserUid(HttpContext);
        var refreshToken = HeadersInfo.GetRefreshToken(HttpContext);
        var md5RefreshToken = HeadersInfo.GetMd5RefreshToken(HttpContext);
        var ipString = IpOperation.GetIpString(HttpContext);
        
        //--Validate init id 
        var visitorInit = await InitializeOperation.ValidateInitIdAsync(_context, initId);
        if (visitorInit.Error != 0)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Invalid Request."
            });
        }
        
        //--Find the User on ProviderLoggedIn Table by userUid #
        var providerReq = await _context.UserProvider.FirstOrDefaultAsync(x=>x.UserUid == userUid && x.Status == ListStatus.STATUS_ACTIVE && x.DeletedAt == 0);
        if (providerReq == null)
        {
            // Find Email on Firebase Provider by User UID
            var firebase = new FirebaseAuthService();
            var firebaseUser = await firebase.GetUserByUidAsync(userUid);
            var email = firebaseUser?.ProviderData[0].Email ?? "";
            
            // Find userProvider by email from UID Email
            var uProviderEmail= await _context.UserProvider.FirstOrDefaultAsync(x=>x.Identity == email && x.IdentityType == UserProviderIdentityTypes.EMAIL && x.DeletedAt == 0);
            
            //-- State
            var state = await UserProfileStates.GetState(_context, providerReq, uProviderEmail);
            if (state.Reference == UserProfileStates.NOT_FOUND && userUid.Length > 20)
            {
                return Ok(new ApiResponse
                {
                    Error = 1,
                    Message = "Invalid Login Information",
                    Reference = "PromptForRegistration"
                });
            }
            if (state.Reference == UserProfileStates.LINK_REQUIRED)
            {
                return Ok(new ApiResponse
                {
                    Error = 2,
                    Message = "Account to Link Required.",
                    Reference = "PromptForLinkWithAccount"
                });
            }

            return Ok(new ApiResponse
            {
                Error = 3,
                Message = "User not found in the system.",
                Reference = "provider"
            });
        }

        var userId = providerReq.UserId;
        var providerId = providerReq.Id;

        var userProfile = await _context.UserProfile.Where(x => x.Id == userId && x.Status == UserProfileStates
            .ACTIVE && x.DeletedAt == 0).FirstOrDefaultAsync();

        if (userProfile == null)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "User not found in the system.",
                Reference = "profile"
            });
        }
        
        // Find Login Log
        var userLoginLog = await _context.UserLoginLog.Where(x => x.UniqueKey == md5RefreshToken && x.DeletedAt == 0)
            .FirstOrDefaultAsync();
        
        //--Login Status Type
        var loginStatus = UserLoginLogStatusTypes.GetType(userLoginLog);
        if (loginStatus == UserLoginLogStatusTypes.LOGGED_IN)
        {
            return Ok(new ApiResponse
            {
                Error = 0,
                Message = "User already logged in",
                Reference = UserLoginLogStatusTypes.LOGGED_IN,
            });
        }
        if (loginStatus != null)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "User login status is invalid",
                Reference = loginStatus,
            });
        }
        
        // Validate Refresh Token with the Firebase
        var firebaseAuth2 = new FirebaseAuth2();
        var firebaseAuth2Validate = await firebaseAuth2.ExchangeRefreshTokenAsync(refreshToken, userUid);
        if (firebaseAuth2Validate == null)
        {
            return Ok(new ApiResponse
            {
                Error = 1,
                Message = "Unable to logging in"
            });
        }
        
        //--crate login log
        var userLoginLogCreate = new UserLoginLog
        {
            UserId = userId,
            ProviderId = providerId,
            UserUid = userUid,
            LoginBy = "refresh_token",
            UniqueKey = md5RefreshToken,
            UserAgent = "",
            ExpiredAt = timeNow + 864000,
            LogoutAt = 0,
            ChecksumBrowser = "",
            Status = UserLoginLogStatusTypes.LOGGED_IN,
            Creator = userId,
            IpString = ipString,
            CreatedAt = timeNow,
            UpdatedAt = timeNow,
            DeletedAt = 0
        };
        _context.UserLoginLog.Add(userLoginLogCreate);
        await _context.SaveChangesAsync();

        userProfile.LastLoginAt = timeNow;
        _context.UserProfile.Update(userProfile);
        await _context.SaveChangesAsync();
        
        return Ok(new ApiResponse
        {
            Error = 0,
            Message = "Login Success",
            Reference = UserLoginLogStatusTypes.LOGGED_IN,
        });
    }
}