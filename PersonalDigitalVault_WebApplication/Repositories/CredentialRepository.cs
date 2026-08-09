using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
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
        public async Task<List<CredentialRecord>> GetAll()
        {
            return await _context.CredentialRecords.ToListAsync();
        }

        // GET BY ID
        public async Task<CredentialRecord?> GetById(int id)
        {
            return await _context.CredentialRecords
                .FindAsync(id);
        }

        // ADD
        public async Task Create(CredentialRecord credential)
        {
            _context.CredentialRecords.Add(credential);

            await _context.SaveChangesAsync();
        }

        // UPDATE
        public async Task Update(int id, CredentialRecord credential)
        {
            _context.CredentialRecords.Update(credential);

            await _context.SaveChangesAsync();
        }

        // DELETE
        public async Task Delete(int id)
        {
            var credential = await _context.CredentialRecords.FindAsync(id);
            if (credential != null)
            {
                _context.CredentialRecords.Remove(credential);
            }

            await _context.SaveChangesAsync();
        }
    }

}
