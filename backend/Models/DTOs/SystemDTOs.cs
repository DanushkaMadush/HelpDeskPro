namespace backend.Models.DTOs
{
    public class SystemDTOs
    {
        public class SystemCreateRequest
        {
            public string SystemName { get; set; } = null!;
            public string CreatedBy { get; set; } = null!;
        }

        public class SystemCreateResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class SystemResponse
        {
            public int SystemId { get; set; }
            public string SystemName { get; set; } = null!;
        }

        public class GetSystemsByUserIdRequest
        {
            public string UserId { get; set; } = null!;
        }

        public class GetSystemsUsersResponse
        {
            public int SysDevId { get; set; }
            public int SystemId { get; set; }
            public string? UserId { get; set; }
            public string? SystemName { get; set; }
        }

        public class GetUsersBySystemIdRequest
        {
            public int SystemId { get; set; }
        }
    }
}
