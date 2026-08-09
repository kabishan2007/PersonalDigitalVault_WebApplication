using PersonalDigitalVault_WebApplication.DTOs.Credential;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface ICredentialService
    {
        Task<List<CredentialDto>> GetAll(int userId);

        Task<CredentialDto?> GetById(int id, int userId);

        Task<string> Create(CredentialDto dto, int userId);

        Task<string> Update(int id, CredentialDto dto, int userId);

        Task<string> Delete(int id, int userId);
    }
}
