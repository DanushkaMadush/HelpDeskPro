using backend.Models.DTOs;

namespace backend.Interface
{
    public interface IDashboardRepository
    {
        Task<DashboardDTOs.KpiResponse> GetKpiAsync();
        Task<IEnumerable<DashboardDTOs.StatusChartResponse>> GetTicketsByStatusAsync();
        Task<IEnumerable<DashboardDTOs.TimeSeriesResponse>> GetTicketsOverTimeAsync();
        Task<IEnumerable<DashboardDTOs.SystemChartResponse>> GetTicketsBySystemAsync();
        Task<IEnumerable<DashboardDTOs.BranchChartResponse>> GetTicketsByBranchAsync();
        Task<IEnumerable<DashboardDTOs.PriorityChartResponse>> GetTicketsByPriorityAsync();
        Task<DashboardDTOs.AvgResolutionResponse> GetAvgResolutionTimeAsync();
    }
}
