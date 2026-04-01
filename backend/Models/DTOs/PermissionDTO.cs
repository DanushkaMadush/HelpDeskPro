namespace backend.Models.DTOs
{
    public class PermissionDTO
    {
        public class CreatePermissionRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }

        public class AssignPermissionRequest
        {
            public string RoleName { get; set; } = string.Empty;
            public string PermissionName { get; set; } = string.Empty;
        }

        public class AssignPermissionToUserRequest
        {
            public string Email { get; set; } = string.Empty;
            public string PermissionName { get; set; } = string.Empty;
        }
    }
}
