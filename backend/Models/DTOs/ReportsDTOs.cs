namespace backend.Models.DTOs
{
    public class ReportsDTOs
    {
        public class StatusSummary
        {
            public string Status { get; set; } = null!;
            public int Total { get; set; }
        }

        public class PrioritySummary
        {
            public string? Priority { get; set; }
            public int Total { get; set; }
        }

        public class SystemSummary
        {
            public string SystemName { get; set; } = null!;
            public int Total { get; set; }
        }

        public class BranchSummary
        {
            public string BranchName { get; set; } = null!;
            public int Total { get; set; }
        }

        public class TicketSummaryResponse
        {
            public List<StatusSummary> Status { get; set; } = new();
            public List<PrioritySummary> Priority { get; set; } = new();
            public List<SystemSummary> System { get; set; } = new();
            public List<BranchSummary> Branch { get; set; } = new();
        }

        public class TicketTrendResponse
        {
            public DateTime Date { get; set; }
            public int TotalTickets { get; set; }
        }

        public class ResolutionDetail
        {
            public int TicketId { get; set; }
            public string? Title { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime ResolvedAt { get; set; }
            public int ResolutionHours { get; set; }
        }

        public class ResolutionSummary
        {
            public string SystemName { get; set; } = null!;
            public double AvgResolutionHours { get; set; }
        }

        public class ResolutionPerformanceResponse
        {
            public List<ResolutionDetail> Details { get; set; } = new();
            public List<ResolutionSummary> Summary { get; set; } = new();
        }
    }
}
