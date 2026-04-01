namespace backend.Models.Entities
{
    public class UserPermission
    {
        public int Id { get; set; }

        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
