using PersonalDigitalVault_WebApplication.DTOs.Folder;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class FolderService: IFolderService
    {
        private readonly IFolderRepository _folderRepository;

        public FolderService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public async Task<List<Folder>> GetAllFolders()
        {
            return await _folderRepository.GetAllAsync();
        }

        public async Task<string> CreateFolder(FolderDto dto)
        {
            var folder = new Folder
            {
                FolderName = dto.FolderName
            };

            await _folderRepository.AddAsync(folder);

            return "Folder Created Successfully";
        }

        public async Task<string> UpdateFolder(int id, FolderDto dto)
        {
            var folder = await _folderRepository.GetByIdAsync(id);

            if (folder == null)
                return "Folder Not Found";

            folder.FolderName = dto.FolderName;

            await _folderRepository.UpdateAsync(folder);

            return "Folder Updated Successfully";
        }

        public async Task<string> DeleteFolder(int id)
        {
            var folder = await _folderRepository.GetByIdAsync(id);

            if (folder == null)
                return "Folder Not Found";

            await _folderRepository.DeleteAsync(folder);

            return "Folder Deleted Successfully";
        }
    }
}
