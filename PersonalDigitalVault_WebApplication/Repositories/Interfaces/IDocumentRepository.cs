

using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetAllAsync();

        Task<Document?> GetByIdAsync(int id);

        Task AddAsync(Document document);

        Task DeleteAsync(Document document);

    }
}
