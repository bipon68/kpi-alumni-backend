using KpiAlumni.Data;
using KpiAlumni.Dtos.Account;
using KpiAlumni.Interfaces;
using KpiAlumni.Models.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace KpiAlumni.Controllers.auth
{
    [ApiController]
    [Route("api/v1/login")]
    public class AuthLoginController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

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
    }

}
