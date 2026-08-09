using PersonalDigitalVault_WebApplication.DTOs.Document;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<List<DocumentDto>> GetAllAsync(int userId);

        Task<List<DocumentDto>> GetByFolderAsync(int folderId, int userId);

        Task<DocumentDto?> GetByIdAsync(int id, int userId);

        Task<(byte[]? FileBytes, string? FileName, string? Message)> DownloadAsync(int id, int userId);

        Task<string> AddAsync(DocumentDto dto, int userId);

        Task<string> DeleteAsync(int id, int userId);
    }
}
