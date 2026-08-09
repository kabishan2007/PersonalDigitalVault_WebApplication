using Microsoft.EntityFrameworkCore;
using PersonalDigitalVault_WebApplication.Data;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;

namespace PersonalDigitalVault_WebApplication.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }
    }
}
