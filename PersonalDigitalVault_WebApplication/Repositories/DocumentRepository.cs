using Microsoft.EntityFrameworkCore;
using PersonalDigitalVault_WebApplication.Data;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;

namespace PersonalDigitalVault_WebApplication.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetAllByUserAsync(int userId)
        {
            return await _context.Documents
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Document>> GetByFolderAsync(int folderId, int userId)
        {
            return await _context.Documents
                .Where(x => x.FolderId == folderId && x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        public async Task AddAsync(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Document document)
        {
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}
