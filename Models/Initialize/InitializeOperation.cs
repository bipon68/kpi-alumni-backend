using KpiAlumni.Data;
using KpiAlumni.Models.Common;
using KpiAlumni.Tables;
using KpiAlumni.Utils;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Models.Initialize;

public static class InitializeOperation
{
    public static async Task<InitProperty> GetInit(AppDbContext _dbContext, HttpContext _httpContext, int userId =0)
    {
        var existingInit = HeadersInfo.GetInitId(_httpContext);

        var initInfo = await _dbContext.VisitorInit.FirstOrDefaultAsync(x => x.InitId == existingInit && x.DeletedAt == 0);

        if (initInfo != null)
        {
            return new InitProperty
            {
                InitId = initInfo.InitId
            };
        }

        var newInitId = StringOperation.MkRandomCode();
        
        // Generate a new init
        var newInit = new VisitorInit
        {
            InitId = newInitId,
            UserId = userId,
            AccessAt = TimeOperation.GetUnixTime(),
            Status = ListStatus.STATUS_ACTIVE,
            Creator = userId,
            IpString = IpOperation.GetIpString(_httpContext),
            CreateAt = TimeOperation.GetUnixTime(),
            UpdateAt = TimeOperation.GetUnixTime()
        };
        
        //Save on DB
        _dbContext.VisitorInit.Add(newInit);
        await _dbContext.SaveChangesAsync();

        return new InitProperty
        {
            InitId = newInitId
        };
    }

    public static async Task<StatusObject> ValidateInitIdAsync(AppDbContext _dbContext, string initId)
    {
        var init = await _dbContext.VisitorInit.FirstOrDefaultAsync(x=>x.InitId == initId && x.DeletedAt == 0);
        if (init == null)
        {
            return new StatusObject
            {
                Error = 1,
                Message = "Invalid Init ID"
            };
        }
        return new StatusObject
        {
            Error = 0,
            Message = "Valid Init ID"
        };
    }
}