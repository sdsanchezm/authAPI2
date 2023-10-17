using AuthenticationAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AuthenticationAPI.Interfaces;


namespace AuthenticationAPI.Services
{
    public class AuthService : IAuthService
    {
        private IConfiguration _configuration;
        private IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return null; // Username already exists
            }

            // Hash password 
            string passwordHash = HashPassword(password);

            // Create new user
            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash
            };

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return null; // Authentication failed
            }

            // return JWT token
            return GenerateJwtToken(user);
        }

        public string GenerateJwtToken(User user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            using (var sha512 = new SHA512Managed())
            {
                var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hashString;
            }
        }

        public bool VerifyPasswordHash(string password, string passwordHash)
        {
            string hashedPassword = HashPassword(password);
            return string.Equals(hashedPassword, passwordHash, StringComparison.OrdinalIgnoreCase);
        }

    }

}
