using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using PersonalDigitalVault_WebApplication.DTOs.Credential;
using PersonalDigitalVault_WebApplication.Models;

namespace PersonalDigitalVault_WebApplication.Repositories.Interfaces
{
    public interface ICredentialRepository
    {
        Task<List<CredentialRecord>> GetAll();
        Task<CredentialRecord?> GetById(int id);
        Task Create(CredentialRecord credential);
        Task Update(int id, CredentialRecord credential);
        Task Delete(int id);
    }


    
}
