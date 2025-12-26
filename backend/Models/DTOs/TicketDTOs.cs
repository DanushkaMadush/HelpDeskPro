namespace backend.Models.DTOs
{
    public class TicketDTOs
    {
        public class CreateTicketRequest
        {
            public required string Title { get; set; }
            public string? Description { get; set; }
            public int BranchId { get; set; }
            public int DepartmentId { get; set; }
            public int SystemId { get; set; }
            public int StatusId { get; set; }
            public int PriorityId { get; set; }
            public required string CreatedBy { get; set; }
        }

        public class CreateTicketResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class TicketResponse
        {
            public int TicketId { get; set; }
            public required string Title { get; set; }
            public string? Description { get; set; }
            public int BranchId { get; set; }
            public string? BranchName { get; set; }
            public int DepartmentId { get; set; }
            public string? DepartmentName { get; set; }
            public int SystemId { get; set; }
            public string? SystemName { get; set; }
            public int StatusId { get; set; }
            public string? Status { get; set; }
            public int PriorityId { get; set; }
            public string? Priority { get; set; }
            public required string CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
            public string? UpdatedBy { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }

        public class GetTicketByIdRequest
        {
            public int TicketId { get; set; }
        }

        public class GetAllTicketsBySystemIdRequest
        {
            public int SystemId { get; set; }
        }

        public class GetAllTicketsByUserIdRequest
        {
            public required string UserId { get; set; }
        }

        public class UpdateTicketStatusRequest
        {
            public int TicketId { get; set; }
            public int StatusId { get; set; }
            public required string UpdatedBy { get; set; }
        }

        public class UpdateTicketStatusResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class SoftDeleteTicketRequest
        {
            public int TicketId { get; set; }
            public required string UpdatedBy { get; set; }
        }

        public class SoftDeleteTicketResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class UpdateTicketDetailsRequest
        {
            public int TicketId { get; set; }
            public required string Title { get; set; }
            public string? Description { get; set; }
            public int BranchId { get; set; }
            public int DepartmentId { get; set; }
            public int SystemId { get; set; }
            public int PriorityId { get; set; }
            public required string UpdatedBy { get; set; }
        }

        public class UpdateTicketDetailsResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class UploadMediaRequest
        {
            public int TicketId { get; set; }
            public IFormFile File { get; set; } = null!;
            public required string UploadedBy { get; set; }
        }

        public class UploadMediaResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public class TicketMediaResponse
        {
            public int TicketMediaId { get; set; }
            public int TicketId { get; set; }
            public string FileName { get; set; } = null!;
            public string OriginalFileName { get; set; } = null!;
            public string FilePath { get; set; } = null!;
            public string MimeType { get; set; } = null!;
            public long FileSize { get; set; }
            public int? DurationSeconds { get; set; }
            public DateTime UploadedAt { get; set; }
        }

        public class DeleteMediaRequest
        {
            public int TicketMediaId { get; set; }
            public required string UpdatedBy { get; set; }
        }

        public class DeleteMediaResponse
        {
            public string? SuccessMessage { get; set; }
            public string? ErrorMessage { get; set; }
        }
    }
}
