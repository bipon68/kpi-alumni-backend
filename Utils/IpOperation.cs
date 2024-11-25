using System.Net;

namespace KpiAlumni.Utils;

public class IpOperation
{
    public static string GetIpString(HttpContext? httpContext)
    {
        try
        {
            var request = httpContext?.Request;
            if (request == null)
            {
                return "";
            }

            // Check if Cloudflare's CF-Connecting-IP header is available
            if (request.Headers.ContainsKey("CF-Connecting-IP"))
            {
                return request.Headers["CF-Connecting-IP"].ToString();
            }

            // Check X-Forwarded-For header set by Nginx or other proxies
            var forwardedIp = request.Headers["X-Forwarded-For"].ToString();
            if (!string.IsNullOrEmpty(forwardedIp))
            {
                // Handle multiple IP addresses in X-Forwarded-For (the first one is the original)
                return forwardedIp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First().Trim();
            }

            // Fall back to the remote IP address from connection
            return request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        }
        catch
        {
            return "";
        }
    }

    public static long GetIpLong(HttpContext? httpContext)
    {
        try
        {
            var request = httpContext?.Request;
            if(request == null)
            {
                return 0;
            }

            var forwardedIp = GetIpString(httpContext);
            var ip = IPAddress.Parse(forwardedIp);
            var ipBytes = ip.GetAddressBytes();
            if (ipBytes.Length == 4)
            {
                return ipBytes[0] << 24 | ipBytes[1] << 16 | ipBytes[2] << 8 | ipBytes[3];
            }

            return 0;
        }
        catch
        {
            return 0;
        }
    }
}