using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.User;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var result = await _userProfileService.GetProfile(userId);

            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(
            int userId,
            UserProfileDto dto)
        {
            var result = await _userProfileService.UpdateProfile(userId, dto);

            return Ok(result);
        }
    }
}
