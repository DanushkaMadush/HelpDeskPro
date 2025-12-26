using backend.Models.Entities;
using static backend.Models.DTOs.UserDTOs;

namespace backend.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(ApplicationUser user, string password);
        Task<string?> LoginAsync(string email, string password);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(string email, string roleName);
        Task<bool> CreatePermissionAsync(string name, string description);
        Task<bool> AssignPermissionToRoleAsync(string roleName, string permissionName);
        Task<bool> AssignPermissionToUserAsync(string email, string permissionName);
        Task<List<UserResponseDto>> GetUsersAsync(string? role);
    }
}
