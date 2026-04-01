namespace backend.Models.DTOs
{
    public class UserDTOs
    {
        public class UserResponseDto
        {
            public string Id { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public IList<string> Roles { get; set; } = new List<string>();
        }
    }
}
