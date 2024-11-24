using Microsoft.AspNetCore.Mvc;

namespace KpiAlumni.Controllers.auth;

public class AuthRegistrationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}