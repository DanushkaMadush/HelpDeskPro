namespace backend.Models.DTOs
{
    public class DepartmentDTOs
    {
        public class DepartmentCreateRequest
        {
            public string DepartmentName { get; set; } = null!;
            public string CreatedBy { get; set; } = null!;
        }

        public class DepartmentCreateResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class DepartmentResponse
        {
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; } = null!;
        }
    }
}
