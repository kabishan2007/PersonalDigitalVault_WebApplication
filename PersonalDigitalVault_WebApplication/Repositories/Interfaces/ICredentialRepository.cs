using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Repositories.Interfaces
{
    public interface ICredentialRepository
    {
        Task<List<CredentialRecord>> GetAllByUser(int userId);

        Task<CredentialRecord?> GetById(int id);

        Task Create(CredentialRecord credential);

        Task Update(CredentialRecord credential);

        Task Delete(int id);
    }


    
}
