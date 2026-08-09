using PersonalDigitalVault_WebApplication.DTOs.Admin;
using PersonalDigitalVault_WebApplication.Repositories.Interfaces;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;

        public AdminService(IAdminRepository repository)
        {
            _repository = repository;
        }

        public async Task<AdminDashboardDto> GetDashboard()
        {
            return new AdminDashboardDto
            {
                TotalUsers = await _repository.GetTotalUsersAsync(),
                TotalFolders = await _repository.GetTotalFoldersAsync(),
                TotalDocuments = await _repository.GetTotalDocumentsAsync(),
                TotalCredentials = await _repository.GetTotalCredentialsAsync()
            };
        }

        public async Task<List<UserManagementDTO>> GetAllUsers()
        {
            var users = await _repository.GetAllUsersAsync();

            return users.Select(u => new UserManagementDTO
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }
    }
}
