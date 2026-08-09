using PersonalDigitalVault_WebApplication.DTOs.User;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class UserProfileService: IUserProfileService
    {
        private readonly IUserProfileRepository _userProfile;

        public UserProfileService(IUserProfileRepository userProfile)
        {
            _userProfile = userProfile;
        }

        public async Task<User?> GetProfile(int userId)
        {
            return await _userProfile.GetByIdAsync(userId);
        }

        public async Task<string> UpdateProfile(
            int userId,
            UserProfileDto dto)
        {
            var user = await _userProfile.GetByIdAsync(userId);

            if (user == null)
                return "User Not Found";

            user.Name = dto.Name;
            user.Email = dto.Email;

            await _userProfile.UpdateAsync(user);

            return "Profile Updated Successfully";
        }


    }
}
