using Microsoft.AspNetCore.Identity;

namespace backend.Models.Entities
{
    public class RolePermission
    {
            public int Id { get; set; }

            public string RoleId { get; set; } = string.Empty;
            public IdentityRole? Role { get; set; }

            public int PermissionId { get; set; }
            public Permission? Permission { get; set; }
    }
}
