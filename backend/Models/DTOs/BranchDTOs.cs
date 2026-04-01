namespace backend.Models.DTOs
{
    public class BranchDTOs
    {
        public class BranchCreateRequest
        {
            public string BranchName { get; set; } = null!;
            public string Address { get; set; } = null!;
            public string CreatedBy { get; set; } = null!;
        }

        public class BranchCreateResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class BranchResponse
        {
            public int BranchId { get; set; }
            public string BranchName { get; set; } = null!;
            public string Address { get; set; } = null!;
        }
    }
}
