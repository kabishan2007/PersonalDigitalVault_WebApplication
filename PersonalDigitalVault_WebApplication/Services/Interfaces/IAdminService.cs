using PersonalDigitalVault_WebApplication.DTOs.Admin;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface IAdminService
    {
        Task<AdminDashboardDto> GetDashboard();

        Task<List<UserManagementDTO>> GetAllUsers();
    }
}
