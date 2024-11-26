using KpiAlumni.Data;
using KpiAlumni.Models.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace KpiAlumni.Controllers.auth;

[ApiController]
[Route("api/v1/registration")]
public class AuthRegistrationController(AppDbContext _context) : Controller
{
    [HttpPost("with-email")]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> RegistrationAsync()
    {
        //-- Validate the request
        //-- Check if the user already exists
        //-- Create a new user
        //-- Send a verification email
        //-- Return a response
        return Ok(new ApiResponse
        {
            Error = 0,
            Message = "Registration Info"
        });
    }
    
    [HttpPost("with-provider")]
    public async Task<ActionResult<IEnumerable<ApiResponse>>> RegistrationWithProviderAsync()
    {
        //-- Validate the request
        //-- Check if the user already exists
        //-- Create a new user
        //-- Send a verification email
        //-- Return a response
        return Ok(new ApiResponse
        {
            Error = 0,
            Message = "Registration Info"
        });
    }
}