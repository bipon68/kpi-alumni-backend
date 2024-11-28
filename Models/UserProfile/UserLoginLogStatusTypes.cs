using KpiAlumni.Tables;
using KpiAlumni.Utils;

namespace KpiAlumni.Models.UserProfile;

public class UserLoginLogStatusTypes
{
    public static string LOGGED_IN = "logged_in"; // User Logged In

    public static string LOGGED_OUT = "logged_out"; // User Logged Out

    public static string AUTO_EXPIRED = "auto_expired"; // Auto Expired

    public static string REVOKED = "revoked"; // Revoked by Admin

    public static string CHECKSUM_MISMATCH = "checksum_mismatch"; // Browser Checksum Mismatch

    public static List<string> GetTypes()
    {
        return [
            LOGGED_IN,
            LOGGED_OUT,
            AUTO_EXPIRED,
            REVOKED
        ];
    }

    public static string? GetType(UserLoginLog? log)
    {
        var timeNow = TimeOperation.GetUnixTime();

        // When no log found
        if (log == null) return null;

        // If Expired
        if (log.ExpiredAt < timeNow) return AUTO_EXPIRED;

        // If Logged Out
        if (log.LogoutAt > 0) return LOGGED_OUT;

        // If Browser Checksum Mismatch
        // todo: recheck
        // if (!log.ChecksumBrowser.Equals(checksumBrowser)) return CHECKSUM_MISMATCH;

        return log.Status;
    }
}