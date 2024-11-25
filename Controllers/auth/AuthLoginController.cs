using KpiAlumni.Data;
using KpiAlumni.Models.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace KpiAlumni.Controllers.auth
{
    [ApiController]
    [Route("api/v1/login")]
    public class AuthLoginController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        
        // GET: api/v1/login
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> GetLoginAsync()
        {
            
            return Ok(new ApiResponse
            {
                Error = 0,
                Message = "Login Info"
            });
        }

        // POST: api/v1/login
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> LoginAsync()
        {
            return Ok(new ApiResponse
            {
                Error = 0,
                Message = "Login Info"
            });
        }
        
        [HttpPost("verify")]
        public Task<ActionResult<IEnumerable<ApiResponse>>> LoginVerifyAsync()
        {
            
            return Task.FromResult<ActionResult<IEnumerable<ApiResponse>>>(Ok(new ApiResponse
            {
                Error = 0,
                Message = "Login Info"
            }));
        }
    }

}
