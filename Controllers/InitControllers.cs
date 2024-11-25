using KpiAlumni.Data;
using KpiAlumni.Models.ApiResponse;
using KpiAlumni.Models.Initialize;
using Microsoft.AspNetCore.Mvc;

namespace KpiAlumni.Controllers
{
    [ApiController]
    [Route("api/v1/init")]
    public class InitControllers(AppDbContext _context) : ControllerBase
    {
        // GET: api/v1/login
        [HttpGet("info")]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> GetInitInfoAsync()
        {
            //Init ID for visitor
            var initInfo = await InitializeOperation.GetInit(_context, HttpContext);
            
            return Ok(new ApiResponse
            {
                Error = 0,
                Message = "Init Info",
                Data = new
                {
                    initInfo.InitId
                }
            });
        }
    }
}
