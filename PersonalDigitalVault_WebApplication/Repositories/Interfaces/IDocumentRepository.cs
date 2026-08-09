using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetAllByUserAsync(int userId);

        Task<List<Document>> GetByFolderAsync(int folderId, int userId);

        Task<Document?> GetByIdAsync(int id);

        Task AddAsync(Document document);

        Task DeleteAsync(Document document);
    }
}
