using backend.Models.DTOs;

namespace backend.Interface
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDTOs.MonthlyTicketResponse>> GetMonthlyTicketsAsync(int year);
        Task<IEnumerable<ReportDTOs.BranchTicketResponse>> GetBranchWiseTicketsAsync();
        Task<IEnumerable<ReportDTOs.PendingSystemResponse>> GetPendingSystemsAsync();
    }
}