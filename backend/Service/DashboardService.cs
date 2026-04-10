using backend.Interface;
using backend.Models.DTOs;

namespace backend.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public Task<DashboardDTOs.KpiResponse> GetKpiAsync() => _repository.GetKpiAsync();

        public Task<IEnumerable<DashboardDTOs.StatusChartResponse>> GetTicketsByStatusAsync() => _repository.GetTicketsByStatusAsync();

        public Task<IEnumerable<DashboardDTOs.TimeSeriesResponse>> GetTicketsOverTimeAsync() => _repository.GetTicketsOverTimeAsync();

        public Task<IEnumerable<DashboardDTOs.SystemChartResponse>> GetTicketsBySystemAsync() => _repository.GetTicketsBySystemAsync();

        public Task<IEnumerable<DashboardDTOs.BranchChartResponse>> GetTicketsByBranchAsync() => _repository.GetTicketsByBranchAsync();

        public Task<IEnumerable<DashboardDTOs.PriorityChartResponse>> GetTicketsByPriorityAsync() => _repository.GetTicketsByPriorityAsync();

        public Task<DashboardDTOs.AvgResolutionResponse> GetAvgResolutionTimeAsync() => _repository.GetAvgResolutionTimeAsync();
    }
}
