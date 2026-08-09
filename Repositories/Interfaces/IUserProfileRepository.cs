using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<User?> GetByIdAsync(int userId);

        Task UpdateAsync(User user);
    }
}
