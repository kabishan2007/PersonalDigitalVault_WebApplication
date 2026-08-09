using PersonalDigitalVault_WebApplication.DTOs.Credential;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IEncryptionService _encryptionService;

        public CredentialService(
            ICredentialRepository credentialRepository,
            IEncryptionService encryptionService)
        {
            _credentialRepository = credentialRepository;
            _encryptionService = encryptionService;
        }

        public async Task<List<CredentialDto>> GetAll(int userId)
        {
            var credentials = await _credentialRepository.GetAllByUser(userId);

            return credentials.Select(MapToDto).ToList();
        }

        public async Task<CredentialDto?> GetById(int id, int userId)
        {
            var credential = await _credentialRepository.GetById(id);

            if (credential == null || credential.UserId != userId)
                return null;

            return MapToDto(credential);
        }

        public async Task<string> Create(CredentialDto dto, int userId)
        {
            var credential = new CredentialRecord
            {
                SiteName = dto.SiteName,
                Username = dto.Username,
                EncryptedPassword = _encryptionService.Encrypt(dto.Password),
                UserId = userId
            };

            await _credentialRepository.Create(credential);

            return "Credential Created Successfully";
        }

        public async Task<string> Update(int id, CredentialDto dto, int userId)
        {
            var credential = await _credentialRepository.GetById(id);

            if (credential == null || credential.UserId != userId)
                return "Credential Not Found";

            credential.SiteName = dto.SiteName;
            credential.Username = dto.Username;
            credential.EncryptedPassword = _encryptionService.Encrypt(dto.Password);

            await _credentialRepository.Update(credential);

            return "Credential Updated Successfully";
        }

        public async Task<string> Delete(int id, int userId)
        {
            var credential = await _credentialRepository.GetById(id);

            if (credential == null || credential.UserId != userId)
                return "Credential Not Found";

            await _credentialRepository.Delete(id);

            return "Credential Deleted Successfully";
        }

        private CredentialDto MapToDto(CredentialRecord credential)
        {
            return new CredentialDto
            {
                CredentialId = credential.CredentialId,
                SiteName = credential.SiteName,
                Username = credential.Username,
                Password = _encryptionService.Decrypt(credential.EncryptedPassword)
            };
        }
    }
}