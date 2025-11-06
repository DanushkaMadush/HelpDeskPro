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

    }
}
