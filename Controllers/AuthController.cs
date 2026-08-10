using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.Auth;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var result = await _authService.Register(dto);

                if (result == "Email already registered")
                    return BadRequest(new { message = result });

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Registration failed. " + GetSafeMessage(ex) });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var result = await _authService.Login(dto);

                if (result == "Invalid Email or Password")
                    return Unauthorized(new { message = result });

                return Ok(new { token = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Login failed. " + GetSafeMessage(ex) });
            }
        }

        private static string GetSafeMessage(Exception ex)
        {
            var text = ex.InnerException?.Message ?? ex.Message;
            if (text.Contains("Cannot open database", StringComparison.OrdinalIgnoreCase))
                return "Database is missing. Restart the application.";
            if (text.Length > 160)
                return text[..160] + "…";
            return text;
        }



    }
}

