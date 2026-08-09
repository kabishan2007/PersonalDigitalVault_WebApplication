using PersonalDigitalVault_WebApplication.DTOs.User;
using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<User?> GetProfile(int userId);

        Task<string> UpdateProfile(
            int userId,
            UserProfileDto dto);
    }
}
