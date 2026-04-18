namespace backend.Models.DTOs
{
    public class ReportDTOs
    {
        public class MonthlyTicketResponse
        {
            public int MonthNumber { get; set; }
            public string MonthName { get; set; } = null!;
            public int TotalTickets { get; set; }
        }

        public class BranchTicketResponse
        {
            public int BranchId { get; set; }
            public string BranchName { get; set; } = null!;
            public int TotalTickets { get; set; }
        }

        public class PendingSystemResponse
        {
            public int SystemId { get; set; }
            public string SystemName { get; set; } = null!;
            public int PendingTickets { get; set; }
        }
    }
}