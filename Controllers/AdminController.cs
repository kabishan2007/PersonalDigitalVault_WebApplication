using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            return Ok(await _service.GetDashboard());
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _service.GetAllUsers());
        }
    }
}
