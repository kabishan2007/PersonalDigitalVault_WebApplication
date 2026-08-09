using Microsoft.EntityFrameworkCore;
using PersonalDigitalVault_WebApplication.Data;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;

namespace PersonalDigitalVault_WebApplication.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTotalFoldersAsync()
        {
            return await _context.Folders.CountAsync();
        }

        public async Task<int> GetTotalDocumentsAsync()
        {
            return await _context.Documents.CountAsync();
        }

        public async Task<int> GetTotalCredentialsAsync()
        {
            return await _context.CredentialRecords.CountAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
