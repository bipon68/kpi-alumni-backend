using FirebaseAdmin.Auth;
using KpiAlumni.Data;
using KpiAlumni.Models.Firebase;
using KpiAlumni.Utils;

namespace KpiAlumni.Models.UserProvider;

public class UserPasswordProviderOperation
{
    public bool Error { get; set; } = false;

        public string Message { get; set; } = "";

        public Tables.UserProvider? userProvider { get; set; } = null;

        public UserRecord? userRecord { get; set; } = null;

        public async Task<UserRecord?> ForceCreatePasswordProvider(HttpContext hContext, AppDbContext _context, string identity, string password, string name)
        {
            // Find provider on Firebase
            var firebaseAuth = new FirebaseAuthService();
            var firebaseUser = await firebaseAuth.GetUserByEmailAsync(identity);

            // Creating Firebase provider or Reset Provider Password
            if (firebaseUser != null)
            {
                // When Firebase User Exists
                // Force Reset Password
                var resetPasswordSt = await firebaseAuth.ResetPasswordAsync(firebaseUser.Uid, password);
                if (resetPasswordSt == false)
                {
                    Error = true;
                    Message = "Error on Registration. Please contact support or use other email address";

                    return null;
                }
            }
            else
            {
                // When Firebase User Not Exists
                // Create Local Provider
                var createProviderSt = await firebaseAuth.CreateUserByEmailAsync(identity, password, name);
                if (createProviderSt == null)
                {
                    Error = true;
                    Message = "Error on Registration. Please contact support or use other email address";

                    return null;
                }

                firebaseUser = createProviderSt;
            }

            // Create Local User Profile
            var localProvider = new Tables.UserProvider
            {
                UserId = 0,
                Provider = FirebaseProviderNames.EMAIL.Key,
                LocalProvider = "firebase",
                DisplayName = name,
                IdentityType = UserProviderIdentityTypes.EMAIL,
                Identity = identity,
                IsVerified = true,
                PhotoUrl = "",
                ProviderUid = firebaseUser.Uid,
                UserUid = firebaseUser.Uid,
                IsHide =false,
                Status = "active",
                IpString = IpOperation.GetIpString(hContext),
                CreatedAt = TimeOperation.GetUnixTime(),
                UpdatedAt = TimeOperation.GetUnixTime(),
                DeletedAt = 0,
            };

            // Save Local Provider
            _context.UserProvider.Add(localProvider);
            await _context.SaveChangesAsync();

            // Return Success
            userProvider = localProvider;
            userRecord = firebaseUser;

            Error = false;
            Message = "Success";

            return firebaseUser;
        }

        public async Task UpdateUserId(AppDbContext _context, int userId)
        {
            if(userProvider == null)
            {
                Error = true;
                Message = "User Provider Not Found";

                return;
            }

            // Update Local Provider
            userProvider.UserId = userId;
            userProvider.Creator = userId;

            // Save Local Provider
            _context.UserProvider.Update(userProvider);
            await _context.SaveChangesAsync();
        }
}