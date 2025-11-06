using backend.Interface;
using backend.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return true;

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            return result.Succeeded;
        }

        public async Task<bool> AssignRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                return false;

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

    }
}
