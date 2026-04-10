namespace backend.Models.DTOs
{
    public class DashboardDTOs
    {
        public class KpiResponse
        {
            public int TotalTickets { get; set; }
            public int PendingTickets { get; set; }
            public int OngoingTickets { get; set; }
            public int CompletedTickets { get; set; }
        }

        public class AvgResolutionResponse
        {
            public double AvgResolutionHours { get; set; }
        }

        public class StatusChartResponse
        {
            public string Status { get; set; }
            public int TicketCount { get; set; }
        }

        public class TimeSeriesResponse
        {
            public DateTime TicketDate { get; set; }
            public int TicketCount { get; set; }
        }

        public class SystemChartResponse
        {
            public string SystemName { get; set; }
            public int TicketCount { get; set; }
        }

        public class BranchChartResponse
        {
            public string BranchName { get; set; }
            public int TicketCount { get; set; }
        }

        public class PriorityChartResponse
        {
            public string Priority { get; set; }
            public int TicketCount { get; set; }
        }
    }
}
