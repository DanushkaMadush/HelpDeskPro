using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using backend.Models.Entities;

namespace backend.Data
{
    public class UserDBContext : IdentityDbContext<ApplicationUser>
    {
        public UserDBContext(DbContextOptions<UserDBContext> options)
            : base(options)
        {
        }
    }
}
