using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Repositories.Interfaces
{
    public interface IFolderRepository
    {
        Task<List<Folder>> GetAllByUserAsync(int userId);

        Task<Folder?> GetByIdAsync(int id);

        Task AddAsync(Folder folder);

        Task UpdateAsync(Folder folder);

        Task DeleteAsync(Folder folder);
    }
}
