using KpiAlumni.Data;
using KpiAlumni.Dtos.Account;
using KpiAlumni.Interfaces;
using KpiAlumni.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NuGet.Common;

namespace KpiAlumni.Controllers.auth
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountRepository _accRepo;
        private readonly AppDbContext _context;
        public LoginController(IAccountRepository accRepo, AppDbContext context)
        {
            _accRepo = accRepo;
            _context = context;
            
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var account=await _accRepo.GetByIdAsync(id);
            if(account == null) 
                return NotFound();
            return Ok(account.ToSignUpDto());

        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Signup([FromBody] CreateSignUpDto createSignUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate password and confirm password match
            if (createSignUp.Password != createSignUp.ConfirmPassword)
            {
                return BadRequest(new { Message = "Password and Confirm Password do not match." });
            }
            // Validate terms and conditions acceptance
            if (!createSignUp.AcceptTermsAndConditions)
            {
                return BadRequest(new { Message = "You must accept the terms and conditions to register." });
            }
            //passing year validation
            if (createSignUp.PassingYear > DateTime.Now.Year)
            {
                return BadRequest(new { Message = "Passing year cannot be in the future." });
            }
            // Check for duplicate email
            if (await _accRepo.EmailExistsAsync(createSignUp.Email))
            {
                return Conflict(new { Message = "Email already exists. Please use another email." });
            }
            // Hash the password
            createSignUp.Password = BCrypt.Net.BCrypt.HashPassword(createSignUp.Password);

            var Accounts = createSignUp.FromCreateSignUpDto();
            await _accRepo.CreateAsync(Accounts);
            return CreatedAtAction(nameof(GetById), new { id = Accounts.Id }, Accounts.ToSignUpDto());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _accRepo.GetUserByUsernameAsync(loginDto.Username);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            // Verify the hashed password
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            return Ok(new
            {
                Message = "Login successful.",
                User = new { user.Id, user.Username, user.Email },
                
            });

        }

    }

}
