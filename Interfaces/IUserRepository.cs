using AuthenticationAPI.Models;

namespace AuthenticationAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}
