using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WDC_project.Core.Entities;
using WDC_project.Core.Helpers;
using WDC_project.Core.Interfaces;
using WDC_project.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WDC_project.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTAuthSettings _JWTAuthSettings;

        public UserService(IUserRepository userRepository, IOptions<JWTAuthSettings> JWTAuthSettings)
        {
            _userRepository = userRepository;
            _JWTAuthSettings = JWTAuthSettings.Value;

        }

        public async Task<int> AddAsync(User user)
        {
            var hasher = new PasswordHasher();
            user.Password = hasher.HashPassword(user.Password);
            user.Roles.Add(new UserRole(){ Role = RolesEntity.User});
            var userId = await _userRepository.AddAsync(user);
            return userId;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task UpdateAsync(User updatedUser)
        {
            await _userRepository.UpdateAsync(updatedUser);
        }

        public async Task DeleteAsync(int userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var hasher = new PasswordHasher();
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Username == username && hasher.VerifyPassword(u.Password, password));

            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWTAuthSettings.Secret);
            var claims = user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Role)).ToList();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(9),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            await _userRepository.UpdateAsync(user);

            return user;

        }

    }
}