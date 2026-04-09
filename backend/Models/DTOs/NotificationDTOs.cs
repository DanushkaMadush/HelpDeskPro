namespace backend.Models.DTOs
{
    public class NotificationDTOs
    {
        public class NotificationMessage
        {
            public string Title { get; set; } = null!;
            public string Message { get; set; } = null!;
            public int? TicketId { get; set; }
            public int? SystemId { get; set; }
            public int? StatusId { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }

        public class GetNotificationByUser
        {
            public int NotificationId { get; set; }
            public string Title { get; set; } = null!;
            public string Message { get; set; } = null!;
            public int? TicketId { get; set; }
            public int? SystemId { get; set; }
            public bool? IsRead { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
    }
}
