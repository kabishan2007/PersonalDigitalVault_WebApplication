using Microsoft.EntityFrameworkCore;
using PersonalDigitalVault_WebApplication.Data;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;

namespace PersonalDigitalVault_WebApplication.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly ApplicationDbContext _context;

        public FolderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Folder>> GetAllByUserAsync(int userId)
        {
            return await _context.Folders
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Folder?> GetByIdAsync(int id)
        {
            return await _context.Folders.FindAsync(id);
        }

        public async Task AddAsync(Folder folder)
        {
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Folder folder)
        {
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Folder folder)
        {
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }
    }
}
