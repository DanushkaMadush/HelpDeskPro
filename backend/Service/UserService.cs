using backend.Interface;
using backend.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;

        public UserService(IUserRepository userRepository, IConfiguration configuration, Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterAsync(ApplicationUser user, string password)
        {
            return await _userRepository.CreateUserAsync(user, password);
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, password, false);
            if (!result.Succeeded)
            {
                return null;
            }

            return GenerateJwtToken(existingUser);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim("userId", user.Id),
                new Claim("plant", user.Plant)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(double.Parse(jwtSettings["ExpireInDays"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
