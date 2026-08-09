using Microsoft.EntityFrameworkCore;
using PersonalDigitalVault_WebApplication.Data;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;

namespace PersonalDigitalVault_WebApplication.Repositories
{
    public class CredentialRepository : ICredentialRepository
    {
        private readonly ApplicationDbContext _context;

        public CredentialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CredentialRecord>> GetAllByUser(int userId)
        {
            return await _context.CredentialRecords
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<CredentialRecord?> GetById(int id)
        {
            return await _context.CredentialRecords.FindAsync(id);
        }

        public async Task Create(CredentialRecord credential)
        {
            _context.CredentialRecords.Add(credential);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CredentialRecord credential)
        {
            _context.CredentialRecords.Update(credential);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var credential = await _context.CredentialRecords.FindAsync(id);

            if (credential != null)
            {
                _context.CredentialRecords.Remove(credential);
                await _context.SaveChangesAsync();
            }
        }
    }

}
