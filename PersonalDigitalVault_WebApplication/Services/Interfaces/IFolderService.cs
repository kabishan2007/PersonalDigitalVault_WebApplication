using PersonalDigitalVault_WebApplication.DTOs.Folder;
using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface IFolderService
    {
        Task<List<Folder>> GetAllFolders();

        Task<string> CreateFolder(FolderDto dto);

        Task<string> UpdateFolder(int id, FolderDto dto);

        Task<string> DeleteFolder(int id);
    }
}
