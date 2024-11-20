using Microsoft.AspNetCore.Mvc;

namespace KpiAlumni.Controllers.auth
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        public IActionResult Login()
        {
            return Ok("Login");

        }

    }

}
