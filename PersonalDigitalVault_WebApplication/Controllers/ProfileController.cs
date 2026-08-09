using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDigitalVault_WebApplication.DTOs.User;
using PersonalDigitalVault_WebApplication.Helpers;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _userProfileService.GetProfile(userId);

            if (result == null)
                return NotFound(new { message = "User Not Found" });

            return Ok(new
            {
                result.UserId,
                result.Name,
                result.Email,
                result.Role
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserProfileDto dto)
        {
            var userId = UserHelper.GetUserId(User);
            var result = await _userProfileService.UpdateProfile(userId, dto);

            if (result == "User Not Found")
                return NotFound(new { message = result });

            return Ok(new { message = result });
        }
    }
}
