using backend.Models.Entities;
using System.Threading.Tasks;

namespace backend.Interface
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(ApplicationUser user, string roleName);
        Task<bool> CreatePermissionAsync(string name, string description);
        Task<bool> AssignPermissionToRoleAsync(string roleName, string permissionName);
        Task<List<string>> GetPermissionsByUserAsync(ApplicationUser user);
        Task<bool> AssignPermissionToUserAsync(string email, string permissionName);
        Task<List<string>> GetPermissionsByUserDirectAsync(ApplicationUser user);
    }
}
