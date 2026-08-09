using PersonalDigitalVault_WebApplication.DTOs.Credential;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly ICredentialRepository _credentialRepository;
        public CredentialService(ICredentialRepository credentialRepository)
        {
            _credentialRepository = credentialRepository;
        }
        public async Task<List<CredentialRecord>> GetAll()
        {
            return await _credentialRepository.GetAll();
        }

        // Get Credential By ID
        public async Task<CredentialRecord?> GetById(int id)
        {
            return await _credentialRepository.GetById(id);
        }

        // Create Credential
        public async Task<string> Create(CredentialDto dto)
        {
            var credential = new CredentialRecord
            {
                SiteName = dto.SiteName,
                Username = dto.Username,
                EncryptedPassword = dto.Password,
                UserId = dto.UserId
            };

            await _credentialRepository.Create(credential);

            return "Credential Created Successfully";
        }

        // Update Credential
        public async Task<string> Update(
            int id,
            CredentialDto dto)
        {
            var credential = await _credentialRepository.GetById(id);

            if (credential == null)
            {
                return "Credential Not Found";
            }

            credential.SiteName = dto.SiteName;
            credential.Username = dto.Username;
            credential.EncryptedPassword = dto.Password;

            await _credentialRepository.Update(id, credential);

            return "Credential Updated Successfully";
        }

        // Delete Credential
        public async Task<string> Delete(int id)
        {
            var credential = await _credentialRepository.GetById(id);

            if (credential == null)
            {
                return "Credential Not Found";
            }

            await _credentialRepository.Delete(id);

            return "Credential Deleted Successfully";
        }
    }
}
