using PersonalDigitalVault_WebApplication.DTOs.Credential;
using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface ICredentialService
    {
        Task<List<CredentialRecord>> GetAll();
        Task<CredentialRecord?> GetById(int id);
        Task<string> Create(CredentialDto dto);
        Task<string> Delete(int id);
        Task<string> Update( int id, CredentialDto dto);
    }
        
        
}
