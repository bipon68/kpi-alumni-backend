using System.Globalization;

namespace KpiAlumni.Utils;

public class TimeOperation
{
    public static long GetUnixTime()
    {
        return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
    
    public static long GetUnixTime(DateTime dateTime)
    {
        return (long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
    
    public static long GetUnixTime(string? dateString, string pattern = "yyyy-MM-dd")
    {
        if (String.IsNullOrEmpty(dateString))
        {
            return 0;
        }

        var date = DateTime.ParseExact(dateString, pattern, CultureInfo.InvariantCulture);
        return GetUnixTime(date);
    }
    
    public static int GetDay(long unixTimeStamp)
    {
        return (int)(new DateTime(1970, 1, 1).AddSeconds(unixTimeStamp)).Day;
    }
    
    public static string GetFullDate(long unixTimeStamp)
    {
        return new DateTime(1970, 1, 1).AddSeconds(unixTimeStamp).ToString("dd MMM yyyy");
    }
    
    public static string GetReadableDate(long unixTimeStamp, string pattern = "yyyy-MM-dd")
    {
        // Return mmm dd, yyyy
        return (new DateTime(1970, 1, 1).AddSeconds(unixTimeStamp)).ToString(pattern);
    }
    
    public static string GetReadableDate(DateTime dTime, string pattern = "yyyy-MM-dd")
    {
        return dTime.ToString(pattern);
    }
    
    public static DateTime GetDateTime(long unixTimeStamp)
    {
        return new DateTime(1970, 1, 1).AddSeconds(unixTimeStamp);
    }
    
    public static int GetClientGmt(HttpRequest request)
    {
        var clientTime = request.Headers["Client-Time"];

        if (string.IsNullOrEmpty(clientTime))
        {
            return 0;
        }

        // "Mon Mar 18 2024 12:55:55 GMT+0600 (Bangladesh Standard Time)"; convert to 60 * 60 * 6 = 21600
        var time = clientTime[0]?.Trim().Split(" ")[5].Substring(3, 3);

        if (time == null)
        {
            return 0;
        }

        return int.Parse(time) * 60 * 60;
    }
    // Alias
    public static long UnixNow()
    {
        return GetUnixTime();
    }
}