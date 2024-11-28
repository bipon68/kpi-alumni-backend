using KpiAlumni.Data;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Models.UserProfile;

public class UserProfileStates
{
    public static string ACTIVE = "active"; // Active User

    public static string INACTIVE = "inactive"; // Temporarily Inactive

    public static string BLOCKED = "blocked"; // UID Forced Blocked

    public static string LINK_REQUIRED = "link_required";

    public static string SUSPENDED = "suspended"; // Suspicious Activity

    public static string NOT_FOUND = "deleted"; // Not found or Deleted

    public static async Task<ApiResponse.ApiResponse> GetState(AppDbContext _context,
        Tables.UserProvider? userProviderRequested, Tables.UserProvider? userProviderByEmail)
    {
        // Find User on UserProfile Table
        var userId = userProviderByEmail?.UserId ?? userProviderRequested?.UserId ?? 0;
        var userProfile = await _context.UserProfile.FirstOrDefaultAsync(x => x.Id == userId && x.DeletedAt == 0);

        if (userProfile == null)
        {
            return new ApiResponse.ApiResponse
            {
                Reference = NOT_FOUND
            };
        }

        if (userProviderRequested == null && userProviderByEmail == null)
        {
            return new ApiResponse.ApiResponse
            {
                Reference = NOT_FOUND,
            };
        }

        // When UserProvider is Active and Requested Provider not found
        if (userProviderRequested == null && userProviderByEmail?.Status == "active")
        {
            return new ApiResponse.ApiResponse
            {
                Reference = LINK_REQUIRED,
            };
        }

        if (userProfile.Status == INACTIVE)
        {
            return new ApiResponse.ApiResponse
            {
                Reference = INACTIVE,
            };
        }

        if (userProfile.Status == BLOCKED)
        {
            return new ApiResponse.ApiResponse
            {
                Reference = BLOCKED,
            };
        }

        if (userProfile.Status == SUSPENDED)
        {
            return new ApiResponse.ApiResponse
            {
                Reference = SUSPENDED,
            };
        }

        if (userProfile.Status == ACTIVE)
        {
            return new ApiResponse.ApiResponse
            {
                Reference = ACTIVE,
            };
        }

        return new ApiResponse.ApiResponse
        {
            Reference = "",
        };
    }
}