using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using KpiAlumni.Configs;

namespace KpiAlumni.Models.Firebase;

public class FirebaseAuthService
{
    public int Error = 1;
    
    public string Message = "No message";
    
    //--Constructor to initialize the FirebaseApp
    public FirebaseAuthService()
    {
        InitializeFirebase();
    }
    // Initialize Firebase app with a service account
    private void InitializeFirebase()
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(FirebaseConfig.GetJson()),
            });
        }
    }
    
    // Method to create a new user by email with full properties
        public async Task<UserRecord?> CreateUserByEmailAsync(string email, string password, string? displayName = null, string? phoneNumber = null, string? photoUrl = null, bool? emailVerified = null)
        {
            try
            {
                // Set user properties using the UserRecordArgs
                UserRecordArgs args = new UserRecordArgs()
                {
                    Email = email,
                    EmailVerified = emailVerified ?? false,
                    Password = password,
                    DisplayName = displayName,
                    PhoneNumber = phoneNumber,
                    // PhotoUrl = photoUrl,
                    Disabled = false
                };

                // Create the user with the specified properties
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

                Error = 0;
                Message = "User created successfully";

                return userRecord;
            }
            catch (FirebaseAuthException ex)
            {

                Error = 1;
                Message = $"Error creating user: {email}. Reason: {ex.Message}";

                return null;
            }
        }

        // Method to create a new user by phone number
        public async Task<UserRecord?> CreateUserByPhoneNumberAsync(string phoneNumber, string? displayName = null, string? photoUrl = null)
        {
            try
            {
                // Set user properties using the UserRecordArgs
                UserRecordArgs args = new UserRecordArgs()
                {
                    PhoneNumber = $"+{phoneNumber}",
                    DisplayName = displayName,
                    // PhotoUrl = photoUrl,
                    Disabled = false
                };

                // Create the user with the specified properties
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

                Error = 1;
                Message = "User created successfully";

                return userRecord;
            }
            catch (FirebaseAuthException ex)
            {
                // throw new Exception($"Error creating user with phone number: {phoneNumber}", ex);

                Error = 1;
                Message = $"Error creating user with phone number: {phoneNumber}. Reason: {ex.Message}";
                return null;
            }
        }

        // Method to get user information by UID
        public async Task<UserRecord?> GetUserByUidAsync(string uid)
        {
            try
            {
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

                Error = 0;
                Message = "User found successfully";

                return userRecord;
            }
            catch (FirebaseAuthException ex)
            {
                // throw new Exception($"Error retrieving user by UID: {uid}", ex);
                Error = 1;
                Message = $"Error retrieving user by UID: {uid}. Reason: {ex.Message}";
                return null;
            }
        }

        // Method to retrieve user information by email address
        public async Task<UserRecord?> GetUserByEmailAsync(string email)
        {
            try
            {
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);

                Error = 1;
                Message = "User found successfully";

                return userRecord;
            }
            catch (FirebaseAuthException ex)
            {
                // throw new Exception($"Error retrieving user by email: {email}", ex);

                Error = 1;
                Message = $"Error retrieving user by email: {email}. Reason: {ex.Message}";
                return null;
            }
        }

        // Method to get user information by phone number
        public async Task<UserRecord?> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserByPhoneNumberAsync($"+{phoneNumber}");

                Error = 0;
                Message = "User found successfully";

                return userRecord;
            }
            catch (FirebaseAuthException ex)
            {
                // throw new Exception($"Error retrieving user by phone number: {phoneNumber}", ex);
                Error = 1;
                Message = $"Error retrieving user by phone number: {phoneNumber}. Reason: {ex.Message}";
                return null;
            }
        }

        // Method to delete a user by UID
        public async Task<bool> DeleteUserAsync(string uid)
        {
            try
            {
                await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);

                Error = 0;
                Message = "User deleted successfully";

                return true;
            }
            catch (FirebaseAuthException ex)
            {
                // throw new Exception($"Error deleting user with UID: {uid}", ex);
                Error = 1;
                Message = $"Error deleting user with UID: {uid}. Reason: {ex.Message}";
                return false;
            }
        }

        // Method to verify Firebase ID token and return the FirebaseToken object
        public async Task<FirebaseToken?> VerifyIdTokenAsync(string idToken)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

                Error = 0;
                Message = "Token verification successful";

                return decodedToken;
            }
            catch (FirebaseAuthException ex)
            {
                Error = 1;
                Message = $"Token verification failed. Reason: {ex.Message}";
                return null;
            }
        }

        public async Task<bool> ResetPasswordAsync(string uid, string newPassword)
        {
            try
            { // Update the user's password
                UserRecordArgs args = new UserRecordArgs()
                {
                    Uid = uid,
                    Password = newPassword // Set the new password here
                };


                // Update the user record with the new password
                await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);

                Error = 0;
                Message = "Password reset successfully";

                return true;
            }
            catch (Exception ex)
            {
                Error = 1;
                Message = $"Error resetting password. Reason: {ex.Message}";

                return false;
            }
        }
}