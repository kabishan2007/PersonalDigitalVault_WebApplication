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
            var result = await _authService.Register(dto);

            if (result == "Email already registered")
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.Login(dto);

            if (result == "Invalid Email or Password")
                return Unauthorized(new { message = result });

            return Ok(new { token = result });
        }
    }
}
