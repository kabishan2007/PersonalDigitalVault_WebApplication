using Microsoft.EntityFrameworkCore;
using PersonalDigitalVault_WebApplication.Data;
using PersonalDigitalVault_WebApplication.DTOs.Auth;
using PersonalDigitalVault_WebApplication.Helpers;
using PersonalDigitalVault_WebApplication.Models;
using PersonalDigitalVault_WebApplication.Services.Interfaces;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            var exists = await _context.Users
                .AnyAsync(x => x.Email == dto.Email);

            if (exists)
                return "Email already registered";

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User Registered Successfully";
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return "Invalid Email or Password";

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            if (!validPassword)
                return "Invalid Email or Password";

            return _jwtHelper.GenerateToken(
                user.UserId,
                user.Email,
                user.Role);
        }
    }
}
