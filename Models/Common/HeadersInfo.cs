using KpiAlumni.Utils;

namespace KpiAlumni.Models.Common;

public class HeadersInfo
{
    public static string GetInitId(HttpContext context)
    {
        return context.Request.Headers["init-id"].ToString();
    }
    public static string GetUserUid(HttpContext context)
    {
        var uid = context.Request.Headers["User-Uid"].ToString().Trim();
        
        return uid.Length > 0 ? uid : "";
    }
    
    public static void SetUserUid(HttpContext hContext, string userUid)
    {
        hContext.Request.Headers["User-Uid"] = userUid;
    }
    
    public static string GetRefreshToken(HttpContext hContext)
    {
        var headerRefreshToken = hContext.Request.Headers["refresh-token"].ToString().Trim();
        
        return headerRefreshToken.Length > 0 ? headerRefreshToken : "";
    }
    
    public static void SetRefreshToken(HttpContext hContext, string refreshToken)
    {
        hContext.Request.Headers["refresh-token"] = refreshToken;
    }
    
    public static string GetMd5RefreshToken(HttpContext hContext)
    {
        var uniqueKey = hContext.Request.Headers["unique-key"].ToString().Trim();
        
        return !string.IsNullOrEmpty(uniqueKey) ? uniqueKey : StringOperation.GenerateMd5(GetRefreshToken(hContext));
    }
}