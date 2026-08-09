using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<int> GetTotalUsersAsync();

        Task<int> GetTotalFoldersAsync();

        Task<int> GetTotalDocumentsAsync();

        Task<int> GetTotalCredentialsAsync();

        Task<List<User>> GetAllUsersAsync();
    }
}
