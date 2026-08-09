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

        // GET ALL DOCUMENTS
        public async Task<List<Document>> GetAllAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        // GET DOCUMENT BY ID
        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        // ADD DOCUMENT
        public async Task AddAsync(Document document)
        {
            _context.Documents.Add(document);

            await _context.SaveChangesAsync();
        }

        // DELETE DOCUMENT
        public async Task DeleteAsync(Document document)
        {
            _context.Documents.Remove(document);

            await _context.SaveChangesAsync();
        }
    }
}