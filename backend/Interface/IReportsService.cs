using backend.Models.DTOs;

namespace backend.Interface
{
    public interface IReportsService
    {
        Task<ReportsDTOs.TicketSummaryResponse> GetTicketSummaryAsync();
        Task<IEnumerable<ReportsDTOs.TicketTrendResponse>> GetTicketTrendsAsync(DateTime? start, DateTime? end, int? systemId, int? branchId);
        Task<ReportsDTOs.ResolutionPerformanceResponse> GetResolutionPerformanceAsync();

    }
}
