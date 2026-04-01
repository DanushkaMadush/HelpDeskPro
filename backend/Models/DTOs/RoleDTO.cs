namespace backend.Models.DTOs
{
    public class RoleDTO
    {
        public class CreateRoleRequest
        {
            public string RoleName { get; set; } = string.Empty;
        }

        public class AssignRoleRequest
        {
            public string Email { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
        }
    }
}
