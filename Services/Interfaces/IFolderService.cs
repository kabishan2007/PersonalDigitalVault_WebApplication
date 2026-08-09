using PersonalDigitalVault_WebApplication.DTOs.Folder;
using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface IFolderService
    {
        Task<List<Folder>> GetAllFolders(int userId);

        Task<Folder?> GetById(int id, int userId);

        Task<string> CreateFolder(FolderDto dto, int userId);

        Task<string> UpdateFolder(int id, FolderDto dto, int userId);

        Task<string> DeleteFolder(int id, int userId);
    }
}
