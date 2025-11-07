using backend.Interface;
using backend.Models.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, IConfiguration configuration, Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
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
            var roles = _userManager.GetRolesAsync(user).Result;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim("userId", user.Id),
                new Claim("plant", user.Plant)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var permissions = _userRepository.GetPermissionsByUserAsync(user).Result;

            foreach (var permission in permissions)
            {
                claims.Add(new Claim("permission", permission));
            }


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(double.Parse(jwtSettings["ExpireInDays"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            return await _userRepository.CreateRoleAsync(roleName);
        }

        public async Task<bool> AssignRoleAsync(string email, string roleName)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return false;

            return await _userRepository.AssignRoleAsync(user, roleName);
        }

        public async Task<bool> CreatePermissionAsync(string name, string description)
        {
            return await _userRepository.CreatePermissionAsync(name, description);
        }

        public async Task<bool> AssignPermissionToRoleAsync(string roleName, string permissionName)
        {
            return await _userRepository.AssignPermissionToRoleAsync(roleName, permissionName);
        }

        public async Task<bool> AssignPermissionToUserAsync(string email, string permissionName)
        {
            return await _userRepository.AssignPermissionToUserAsync(email, permissionName);
        }



    }
}
