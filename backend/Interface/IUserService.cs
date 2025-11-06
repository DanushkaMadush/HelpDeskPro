using backend.Models.Entities;

namespace backend.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(ApplicationUser user, string password);
        Task<string?> LoginAsync(string email, string password);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(string email, string roleName);

    }
}
