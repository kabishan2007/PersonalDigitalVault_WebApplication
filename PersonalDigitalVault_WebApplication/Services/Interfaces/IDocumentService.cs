using PersonalDigitalVault_WebApplication.DTOs.Document;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<List<DocumentDto>> GetAllAsync();

        Task<DocumentDto?> GetByIdAsync(int id);

        Task<string> AddAsync(DocumentDto dto);

        Task<string> DeleteAsync(int id);
    }
}
