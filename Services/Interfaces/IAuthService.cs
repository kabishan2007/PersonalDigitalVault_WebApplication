using PersonalDigitalVault_WebApplication.DTOs.Auth;

namespace PersonalDigitalVault_WebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDto dto);

        Task<string> Login(LoginDto dto);
    }
}
