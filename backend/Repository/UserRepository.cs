using backend.Data;
using backend.Interface;
using backend.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserDBContext _db;


        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, UserDBContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
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

        public async Task<bool> CreatePermissionAsync(string name, string description)
        {
            if (_db.Permissions.Any(p => p.Name == name))
                return true;

            _db.Permissions.Add(new Permission { Name = name, Description = description });
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignPermissionToRoleAsync(string roleName, string permissionName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;

            var permission = _db.Permissions.FirstOrDefault(p => p.Name == permissionName);
            if (permission == null) return false;

            _db.RolePermissions.Add(new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            });

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetPermissionsByUserAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = _db.RolePermissions
                .Where(rp => roles.Contains(rp.Role!.Name!))
                .Select(rp => rp.Permission!.Name)
                .Distinct()
                .ToList();

            return permissions;
        }

        public async Task<bool> AssignPermissionToUserAsync(string email, string permissionName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var permission = _db.Permissions.FirstOrDefault(p => p.Name == permissionName);
            if (permission == null) return false;

            _db.UserPermissions.Add(new UserPermission
            {
                UserId = user.Id,
                PermissionId = permission.Id
            });

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetPermissionsByUserDirectAsync(ApplicationUser user)
        {
            var permissionsViaRoles = _db.RolePermissions
                .Where(rp => _userManager.GetRolesAsync(user).Result.Contains(rp.Role!.Name!))
                .Select(rp => rp.Permission!.Name)
                .ToList();

            var directPermissions = await GetPermissionsByUserDirectAsync(user);

            return permissionsViaRoles.Concat(directPermissions).Distinct().ToList();
        }
    }
}
