using PersonalDigitalVault_WebApplication.DTOs.Folder;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;

        public FolderService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        public async Task<List<Folder>> GetAllFolders(int userId)
        {
            return await _folderRepository.GetAllByUserAsync(userId);
        }

        public async Task<Folder?> GetById(int id, int userId)
        {
            var folder = await _folderRepository.GetByIdAsync(id);

            if (folder == null || folder.UserId != userId)
                return null;

            return folder;
        }

        public async Task<string> CreateFolder(FolderDto dto, int userId)
        {
            var folder = new Folder
            {
                FolderName = dto.FolderName,
                UserId = userId
            };

            await _folderRepository.AddAsync(folder);

            return "Folder Created Successfully";
        }

        public async Task<string> UpdateFolder(int id, FolderDto dto, int userId)
        {
            var folder = await _folderRepository.GetByIdAsync(id);

            if (folder == null || folder.UserId != userId)
                return "Folder Not Found";

            folder.FolderName = dto.FolderName;

            await _folderRepository.UpdateAsync(folder);

            return "Folder Updated Successfully";
        }

        public async Task<string> DeleteFolder(int id, int userId)
        {
            var folder = await _folderRepository.GetByIdAsync(id);

            if (folder == null || folder.UserId != userId)
                return "Folder Not Found";

            await _folderRepository.DeleteAsync(folder);

            return "Folder Deleted Successfully";
        }
    }
}
