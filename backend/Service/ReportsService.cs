using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _repository;

        public ReportsService(IReportsRepository repository)
        {
            _repository = repository;
        }

        public Task<ReportsDTOs.TicketSummaryResponse> GetTicketSummaryAsync()
        {
            return _repository.GetTicketSummaryAsync();
        }

        public Task<IEnumerable<ReportsDTOs.TicketTrendResponse>> GetTicketTrendsAsync(DateTime? start, DateTime? end, int? systemId, int? branchId)
        {
            return _repository.GetTicketTrendsAsync(start, end, systemId, branchId);
        }

        public Task<ReportsDTOs.ResolutionPerformanceResponse> GetResolutionPerformanceAsync()
        {
            return _repository.GetResolutionPerformanceAsync();
        }
    }
}
