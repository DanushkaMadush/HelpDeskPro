using backend.Models.Entities;
using System.Threading.Tasks;

namespace backend.Interface
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
    }
}
