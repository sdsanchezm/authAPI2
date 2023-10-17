using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using AuthenticationAPI.Models;

namespace AuthenticationAPI.Interfaces
{
    public interface IAuthService
    {
        //Task<User> RegisterAsync(RegisterRequest model);
        Task<User> RegisterAsync(string username, string password);
        //Task<User> LoginAsync(LoginRequest model);
        Task<string> LoginAsync(string username, string password);
        // Task LogoutAsync();
    }
}
