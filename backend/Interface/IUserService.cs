using backend.Models.Entities;

namespace backend.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(ApplicationUser user, string password);
        Task<string?> LoginAsync(string email, string password);
    }
}
